using System;
using System.IO;
using System.Numerics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptoUtility
{
	/// <summary>
	/// RSA PEM format key pair analysis and export
	/// GitHub: https://github.com/xiangyuecn/RSA-csharp
	/// </summary>
	public class PEM
	{
		static private readonly Regex _PEMCode = new Regex(@"--+.+?--+|\s+");
		static private readonly byte[] _SeqOID = new byte[] { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
		static private readonly byte[] _Ver = new byte[] { 0x02, 0x01, 0x00 };

		static private readonly Regex xmlExp = new Regex("\\s*<RSAKeyValue>([<>\\/\\+=\\w\\s]+)</RSAKeyValue>\\s*");
		static private readonly Regex xmlTagExp = new Regex("<(.+?)>\\s*([^<]+?)\\s*</");


		/// <summary>
		/// modulus modulus n, both public and private keys
		/// </summary>
		public byte[] Key_Modulus;
		/// <summary>
		/// publicExponent public key exponent e, both public and private keys
		/// </summary>
		public byte[] Key_Exponent;
		/// <summary>
		/// privateExponent private key exponent d, only available when private key
		/// </summary>
		public byte[] Key_D;

		//The following parameters are only available for private keys https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.rsaparameters?redirectedfrom=MSDN&view=netframework-4.8
		/// <summary>
		/// prime1
		/// </summary>
		public byte[] Val_P;
		/// <summary>
		/// prime2
		/// </summary>
		public byte[] Val_Q;
		/// <summary>
		/// exponent1
		/// </summary>
		public byte[] Val_DP;
		/// <summary>
		/// exponent2
		/// </summary>
		public byte[] Val_DQ;
		/// <summary>
		/// coefficient
		/// </summary>
		public byte[] Val_InverseQ;

		private PEM() { }

		/// <summary>
		/// Construct a PEM from the public key and private key in RSA, if convertToPublic RSA with private key will only read the public key, RSA with only public key will not be affected
		/// </summary>
		public PEM(RSACryptoServiceProvider rsa, bool convertToPublic = false) 
		{
			var isPublic = convertToPublic || rsa.PublicOnly;
			var param = rsa.ExportParameters(!isPublic);

			Key_Modulus = param.Modulus;
			Key_Exponent = param.Exponent;

			if (!isPublic) 
			{
				Key_D = param.D;

				Val_P = param.P;
				Val_Q = param.Q;
				Val_DP = param.DP;
				Val_DQ = param.DQ;
				Val_InverseQ = param.InverseQ;
			}
		}

		/// <summary>
		/// Construct a PEM from the full amount of PEM field data. Except for modulus and public key exponent, all other private key exponent information must be provided or not (the exported PEM contains only the public key)
		/// Note: If the first byte of all parameters is 0, you must remove it first
		/// </summary>
		public PEM(byte[] modulus, byte[] exponent, byte[] d, byte[] p, byte[] q, byte[] dp, byte[] dq, byte[] inverseQ) 
		{
			Key_Modulus = modulus;
			Key_Exponent = exponent;
			Key_D = BigL(d, modulus.Length);

			int keyLen = modulus.Length / 2;
			Val_P = BigL(p, keyLen);
			Val_Q = BigL(q, keyLen);
			Val_DP = BigL(dp, keyLen);
			Val_DQ = BigL(dq, keyLen);
			Val_InverseQ = BigL(inverseQ, keyLen);
		}

		/// <summary>
		/// Construct a PEM from the public key index and the private key index, and calculate P and Q in reverse, but they are very unlikely to be the same as the P and Q of the original generated key
		/// Note: If the first byte of all parameters is 0, you must remove it first
		/// Error will throw an exception
		/// </summary>
		/// <param name="modulus">must provide modulus</param>
		/// <param name="exponent">Public key exponent must be provided</param>
		/// <param name="dOrNull">The private key index may not be provided, and the exported PEM only contains the public key</param> 
		public PEM(byte[] modulus, byte[] exponent, byte[] dOrNull)
		{
			Key_Modulus = modulus;//modulus
			Key_Exponent = exponent;//publicExponent

			if (dOrNull != null) 
			{
				Key_D = BigL(dOrNull, modulus.Length);//privateExponent

				// Reverse P, Q
				BigInteger n = BigX(modulus);
				BigInteger e = BigX(exponent);
				BigInteger d = BigX(dOrNull);
				BigInteger p = FindFactor(e, d, n);
				BigInteger q = n / p;
				if (p.CompareTo(q) > 0)
				{
					BigInteger t = p;
					p = q;
					q = t;
				}
				BigInteger exp1 = d % (p - BigInteger.One);
				BigInteger exp2 = d % (q - BigInteger.One);
				BigInteger coeff = BigInteger.ModPow(q, p - 2, p);

				int keyLen = modulus.Length / 2;
				Val_P = BigL(BigB(p), keyLen);//prime1
				Val_Q = BigL(BigB(q), keyLen);//prime2
				Val_DP = BigL(BigB(exp1), keyLen);//exponent1
				Val_DQ = BigL(BigB(exp2), keyLen);//exponent2
				Val_InverseQ = BigL(BigB(coeff), keyLen);//coefficient
			}
		}

		/// <summary>
		/// Key bits
		/// </summary>
		public int KeySize 
		{
			get {return Key_Modulus.Length * 8;	}
		}

		/// <summary>
		/// Whether to include the private key
		/// </summary>
		public bool HasPrivate
		{
			get {return Key_D != null;}
		}

		/// <summary>
		/// Convert the public key private key in the PEM into an RSA object. If the private key is not provided, only the public key will be included in the RSA
		/// </summary>
		public RSACryptoServiceProvider GetRSA() 
		{
			var rsaParams = new CspParameters();
			rsaParams.Flags = CspProviderFlags.UseMachineKeyStore;
			var rsa = new RSACryptoServiceProvider(rsaParams);

			var param = new RSAParameters();
			param.Modulus = Key_Modulus;
			param.Exponent = Key_Exponent;
			if (Key_D != null) 
			{
				param.D = Key_D;
				param.P = Val_P;
				param.Q = Val_Q;
				param.DP = Val_DP;
				param.DQ = Val_DQ;
				param.InverseQ = Val_InverseQ;
			}
			rsa.ImportParameters(param);
			return rsa;
		}

		/// <summary>
		/// Convert to a positive integer, if it is a negative number, you need to add a leading 0 to convert to a positive integer
		/// </summary>
		static public BigInteger BigX(byte[] bigb)
		{
			if (bigb[0] > 0x7F) 
			{
				byte[] c = new byte[bigb.Length + 1];
				Array.Copy(bigb, 0, c, 1, bigb.Length);
				bigb = c;
			}
			return new BigInteger(bigb.Reverse().ToArray());// C#'s binary is reversed
		}

		/// <summary>
		/// The first byte of BigInt exported byte integer> 0x7F will add a leading 0 to ensure a positive integer, so 0 needs to be removed
		/// </summary>
		static public byte[] BigB(BigInteger bigx) 
		{
			byte[] val = bigx.ToByteArray().Reverse().ToArray();// C#'s binary is reversed
			if (val[0] == 0) {
				byte[] c = new byte[val.Length - 1];
				Array.Copy(val, 1, c, 0, c.Length);
				val = c;
			}
			return val;
		}

		/// <summary>
		/// Some key parameters may be less than one bit (32 bytes only have 31. Visual inspection is a problem with the key generator. This parameter is only found in the key generated by c#, but the key generated in java does not This phenomenon), just correct it directly; this problem is fundamentally different from BigB, and BigB cannot be touched.
		/// </summary>
		static public byte[] BigL(byte[] bytes, int keyLen) 
		{
			if (keyLen - bytes.Length == 1) 
			{
				byte[] c = new byte[bytes.Length + 1];
				Array.Copy(bytes, 0, c, 1, bytes.Length);
				bytes = c;
			}
			return bytes;
		}

		/// <summary>
		/// Get P Q from n e d
		/// Information: https://stackoverflow.com/questions/43136036/how-to-get-a-rsaprivatecrtkey-from-a-rsaprivatekey
		/// https://v2ex.com/t/661736
		/// </summary>
		static private BigInteger FindFactor(BigInteger e, BigInteger d, BigInteger n)
		{
			BigInteger edMinus1 = e * d - BigInteger.One;
			int s = -1;
			if (edMinus1 != BigInteger.Zero) 
			{
				s = (int)(BigInteger.Log(edMinus1 & -edMinus1) / BigInteger.Log(2));
			}
			BigInteger t = edMinus1 >> s;

			long now = DateTime.Now.Ticks;
			for (int aInt = 2; true; aInt++) 
			{
				if (aInt % 10 == 0 && DateTime.Now.Ticks - now > 3000 * 10000)
				{
					throw new Exception("estimate RSA.P timeout");//The test loops up to 2 times, and the 1024-bit speed is very fast 8ms
				}

				BigInteger aPow = BigInteger.ModPow(new BigInteger(aInt), t, n);
				for (int i = 1; i <= s; i++) 
				{
					if (aPow == BigInteger.One) 
					{
						break;
					}
					if (aPow == n - BigInteger.One)
					{
						break;
					}
					BigInteger aPowSquared = aPow * aPow % n;
					if (aPowSquared == BigInteger.One) 
					{
						return BigInteger.GreatestCommonDivisor(aPow - BigInteger.One, n);
					}
					aPow = aPowSquared;
				}
			}
		}


		/// <summary>
		/// Create RSA with PEM format key pair, support PEM in PKCS#1, PKCS#8 format
		/// Error will throw an exception
		/// </summary>
		static public PEM FromPEM(string pem) 
		{
			PEM param = new PEM();

			var base64 = _PEMCode.Replace(pem, "");
			byte[] data = null;
			try { data = Convert.FromBase64String(base64); } catch { }
			if (data == null)
			{
				throw new Exception("PEM content is invalid");
			}
			var idx = 0;

			// read length
			Func<byte, int> readLen = (first) =>
			{
				if (data[idx] == first) 
				{
					idx++;
					if (data[idx] == 0x81)
					{
						idx++;
						return data[idx++];
					} else if (data[idx] == 0x82) 
					{
						idx++;
						return (((int)data[idx++]) << 8) + data[idx++];
					} else if (data[idx] < 0x80)
					{
						return data[idx++];
					}
				}
				throw new Exception("PEM failed to extract data");
			};

			//Read block data
			Func<byte[]> readBlock = () =>
			{
				var len = readLen(0x02);
				if (data[idx] == 0x00) {
					idx++;
					len--;
				}
				var val = new byte[len];
				for (var i = 0; i < len; i++) 
				{
					val[i] = data[idx + i];
				}
				idx += len;
				return val;
			};

			// Compare whether data is byts content starting from idx position
			Func<byte[], bool> eq = (byts) => 
			{
				for (var i = 0; i < byts.Length; i++, idx++)
				{
					if (idx >= data.Length)
					{
						return false;
					}
					if (byts[i] != data[idx])
					{
						return false;
					}
				}
				return true;
			};

			if (pem.Contains("PUBLIC KEY"))
			{
				/****Use public key****/
				//Total length of read data
				readLen(0x30);

				//Detect PKCS8
				var idx2 = idx;
				if (eq(_SeqOID))
				{
					//Read 1 length
					readLen(0x03);
					idx++;//skip 0x00
						  //Read 2 length
					readLen(0x30);
				}
				else
				{
					idx = idx2;
				}

				//Modulus
				param.Key_Modulus = readBlock();

				//Exponent
				param.Key_Exponent = readBlock();
			} 
			else if (pem.Contains("PRIVATE KEY")) 
			{
				/****Use private key****/
				//Total length of read data
				readLen(0x30);

				//Read the version number
				if (!eq(_Ver))
				{
					throw new Exception("PEM unknown version");
				}

				//Detect PKCS8
				var idx2 = idx;
				if (eq(_SeqOID))
				{
					//Read 1 length
					readLen(0x04);
					//Read 2 length
					readLen(0x30);

					//Read the version number
					if (!eq(_Ver))
					{
						throw new Exception("PEM version is invalid");
					}
				}
				else
				{
					idx = idx2;
				}

				//Read data 
				param.Key_Modulus = readBlock();
				param.Key_Exponent = readBlock();
				int keyLen = param.Key_Modulus.Length;
				param.Key_D = BigL(readBlock(), keyLen);
				keyLen = keyLen / 2;
				param.Val_P = BigL(readBlock(), keyLen);
				param.Val_Q = BigL(readBlock(), keyLen);
				param.Val_DP = BigL(readBlock(), keyLen);
				param.Val_DQ = BigL(readBlock(), keyLen);
				param.Val_InverseQ = BigL(readBlock(), keyLen);
			} 
			else 
			{
				throw new Exception("pem requires BEGIN END header");
			}

			return param;
		}


		/// <summary>
		/// Convert the key pair in RSA into PEM PKCS#1 format
		/// convertToPublic: When equal to true, the RSA with the private key will only return the public key, and the RSA with only the public key will not be affected
		/// Public key such as: -----BEGIN RSA PUBLIC KEY-----, private key such as: -----BEGIN RSA PRIVATE KEY-----
		/// It seems that the public key of PKCS#1 is less used to export, the public key of PKCS#8 is used more, and the private key #1#8 is almost the same.
		/// </summary>
		public string ToPEM_PKCS1(bool convertToPublic = false)
		{
			return ToPEM(convertToPublic, false, false);
		}

		/// <summary>
		/// Convert the key pair in RSA into PEM PKCS#8 format
		/// convertToPublic: When equal to true, the RSA with the private key will only return the public key, and the RSA with only the public key will not be affected
		/// Public key such as: -----BEGIN PUBLIC KEY-----, private key such as: -----BEGIN PRIVATE KEY-----
		/// </summary>
		public string ToPEM_PKCS8(bool convertToPublic = false)
		{
			return ToPEM(convertToPublic, true, true);
		}

		/// <summary>
		/// Convert the key pair in RSA into PEM format
		/// convertToPublic: When equal to true, the RSA with the private key will only return the public key, and the RSA with only the public key will not be affected
		/// privateUsePKCS8: The return format of the private key. When it is true, it will return PKCS#8 format (-----BEGIN PRIVATE KEY-----), otherwise it will return PKCS#1 format (-----BEGIN RSA PRIVATE KEY-- ---), this parameter is invalid when returning the public key; both formats are more common
		/// publicUsePKCS8: The return format of the public key. When it is true, it returns PKCS#8 format (-----BEGIN PUBLIC KEY-----), otherwise it returns PKCS#1 format (-----BEGIN RSA PUBLIC KEY-- ---), this parameter is invalid when returning the private key; generally, the true PKCS#8 format public key is used, and the PKCS#1 format seems to be relatively rare.
		/// </summary>
		public string ToPEM(bool convertToPublic, bool privateUsePKCS8, bool publicUsePKCS8)
		{
			//https://www.jianshu.com/p/25803dd9527d
			//https://www.cnblogs.com/ylz8401/p/8443819.html
			//https://blog.csdn.net/jiayanhui2877/article/details/47187077
			//https://blog.csdn.net/xuanshao_/article/details/51679824
			//https://blog.csdn.net/xuanshao_/article/details/51672547

			var ms = new MemoryStream();

			//Write a length bytecode
			Action<int> writeLenByte = (len) => 
			{
				if (len < 0x80)
				{
					ms.WriteByte((byte)len);
				}
				else if (len <= 0xff)
				{
					ms.WriteByte(0x81);
					ms.WriteByte((byte)len);
				}
				else
				{
					ms.WriteByte(0x82);
					ms.WriteByte((byte)(len >> 8 & 0xff));
					ms.WriteByte((byte)(len & 0xff));
				}
			};

			//Write a piece of data
			Action<byte[]> writeBlock = (byts) =>
			{
				var addZero = (byts[0] >> 4) >= 0x8;
				ms.WriteByte(0x02);
				var len = byts.Length + (addZero ? 1 : 0);
				writeLenByte(len);

				if (addZero)
				{
					ms.WriteByte(0x00);
				}
				ms.Write(byts, 0, byts.Length);
			};

			//Write length data according to the length of the subsequent content
			Func<int, byte[], byte[]> writeLen = (index, byts) =>
			{
				var len = byts.Length - index;

				ms.SetLength(0);
				ms.Write(byts, 0, index);
				writeLenByte(len);
				ms.Write(byts, index, len);

				return ms.ToArray();
			};

			Action<MemoryStream, byte[]> writeAll = (stream, byts) =>
			{
				stream.Write(byts, 0, byts.Length);
			};

			Func<string, int, string> TextBreak = (text, line) => 
			{
				var idx = 0;
				var len = text.Length;
				var str = new StringBuilder();
				while (idx < len) {
					if (idx > 0) {
						str.Append('\n');
					}
					if (idx + line >= len) {
						str.Append(text.Substring(idx));
					} else {
						str.Append(text.Substring(idx, line));
					}
					idx += line;
				}
				return str.ToString();
			};


			if (Key_D == null || convertToPublic) 
			{
				/****Generate public key****/

				//Total number of bytes written, excluding the length of this section, an additional 24 bytes of header is required, which will be filled in after calculation
				ms.WriteByte(0x30);
				var index1 = (int)ms.Length;

				//PKCS8 one more piece of data
				int index2 = -1, index3 = -1;
				if (publicUsePKCS8)
				{
					//Fixed content
					// encoded OID sequence for PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
					writeAll(ms, _SeqOID);

					//Subsequent length starting from 0x00
					ms.WriteByte(0x03);
					index2 = (int)ms.Length;
					ms.WriteByte(0x00);

					//Follow-up content length
					ms.WriteByte(0x30);
					index3 = (int)ms.Length;
				}

				//Write to Modulus
				writeBlock(Key_Modulus);

				//Write Exponent
				writeBlock(Key_Exponent);


				//Calculate the length of the vacancy
				var byts = ms.ToArray();

				if (index2 != -1)
				{
					byts = writeLen(index3, byts);
					byts = writeLen(index2, byts);
				}
				byts = writeLen(index1, byts);


				var flag = "PUBLIC KEY";
				if (!publicUsePKCS8)
				{
					flag = "RSA" + flag;
				}
				return "-----BEGIN" + flag + "-----\n" + TextBreak(Convert.ToBase64String(byts), 64) + "\n-----END" + flag + "-- ---";
			}
			else
			{
				/****Generate private key****/

				//Total number of bytes written, subsequent write
				ms.WriteByte(0x30);
				int index1 = (int)ms.Length;

				//Write the version number
				writeAll(ms, _Ver);

				//PKCS8 one more piece of data
				int index2 = -1, index3 = -1;
				if (privateUsePKCS8)
				{
					//Fixed content
					writeAll(ms, _SeqOID);

					//Follow-up content length
					ms.WriteByte(0x04);
					index2 = (int)ms.Length;

					//Follow-up content length
					ms.WriteByte(0x30);
					index3 = (int)ms.Length;

					//Write the version number
					writeAll(ms, _Ver);
				}

				//data input
				writeBlock(Key_Modulus);
				writeBlock(Key_Exponent);
				writeBlock(Key_D);
				writeBlock(Val_P);
				writeBlock(Val_Q);
				writeBlock(Val_DP);
				writeBlock(Val_DQ);
				writeBlock(Val_InverseQ);


				//Calculate the length of the vacancy
				var byts = ms.ToArray();

				if (index2 != -1)
				{
					byts = writeLen(index3, byts);
					byts = writeLen(index2, byts);
				}
				byts = writeLen(index1, byts);


				var flag = " PRIVATE KEY";
				if (!privateUsePKCS8)
				{
					flag = " RSA" + flag;
				}
				return "-----BEGIN" + flag + "-----\n" + TextBreak(Convert.ToBase64String(byts), 64) + "\n-----END" + flag + "-----";
			}
		}


		/// <summary>
		/// Convert XML format key to PEM, support public key xml, private key xml
		/// Error will throw an exception
		/// </summary>
		static public PEM FromXML(string xml) 
		{
			PEM rtv = new PEM();

			Match xmlM = xmlExp.Match(xml);
			if (!xmlM.Success)
			{
				throw new Exception("XML content does not meet the requirements");
			}

			Match tagM = xmlTagExp.Match(xmlM.Groups[1].Value);
			while (tagM.Success) 
			{
				string tag = tagM.Groups[1].Value;
				string b64 = tagM.Groups[2].Value;
				byte[] val = Convert.FromBase64String(b64);
				switch (tag) 
				{
					case "Modulus": rtv.Key_Modulus = val; break;
					case "Exponent": rtv.Key_Exponent = val; break;
					case "D": rtv.Key_D = val; break;

					case "P": rtv.Val_P = val; break;
					case "Q": rtv.Val_Q = val; break;
					case "DP": rtv.Val_DP = val; break;
					case "DQ": rtv.Val_DQ = val; break;
					case "InverseQ": rtv.Val_InverseQ = val; break;
				}
				tagM = tagM.NextMatch();
			}

			if (rtv.Key_Modulus == null || rtv.Key_Exponent == null)
			{
				throw new Exception("XML public key is lost");
			}

			if (rtv.Key_D != null) 
			{
				if (rtv.Val_P == null || rtv.Val_Q == null || rtv.Val_DP == null || rtv.Val_DQ == null || rtv.Val_InverseQ == null)
				{
					return new PEM(rtv.Key_Modulus, rtv.Key_Exponent, rtv.Key_D);
				}
			}

			return rtv;
		}


		/// <summary>
		/// Convert the key pair in RSA into XML format
		///, if convertToPublic RSA with private key will only return public key, RSA with only public key will not be affected
		/// </summary>
		public string ToXML(bool convertToPublic)
		{
			StringBuilder str = new StringBuilder();
			str.Append("<RSAKeyValue>");
			str.Append("<Modulus>" + Convert.ToBase64String(Key_Modulus) + "</Modulus>");
			str.Append("<Exponent>" + Convert.ToBase64String(Key_Exponent) + "</Exponent>");
			if (Key_D == null || convertToPublic)
			{
				/****Generate public key****/
				//NOOP
			}
			else
			{
				/****Generate private key****/
				str.Append("<P>" + Convert.ToBase64String(Val_P) + "</P>");
				str.Append("<Q>" + Convert.ToBase64String(Val_Q) + "</Q>");
				str.Append("<DP>" + Convert.ToBase64String(Val_DP) + "</DP>");
				str.Append("<DQ>" + Convert.ToBase64String(Val_DQ) + "</DQ>");
				str.Append("<InverseQ>" + Convert.ToBase64String(Val_InverseQ) + "</InverseQ>");
				str.Append("<D>" + Convert.ToBase64String(Key_D) + "</D>");
			}
			str.Append("</RSAKeyValue>");
			return str.ToString();
		}

	}
}
