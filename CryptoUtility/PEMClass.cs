using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509;
using PemObject = Org.BouncyCastle.Utilities.IO.Pem.PemObject;
using IBigInteger = Org.BouncyCastle.Math.BigInteger;
using System.Numerics;

namespace CryptoUtility
{
    class PEMClass
    {
        /// <summary>
        /// Import OpenSSH PEM private key string into MS RSACryptoServiceProvider
        /// </summary>
        /// <param name="pem"></param>
        /// <returns></returns>
        public static RSACryptoServiceProvider cImportPrivateKey(string pem)
        {
            PemReader pr = new PemReader(new StringReader(pem));
            AsymmetricCipherKeyPair KeyPair = (AsymmetricCipherKeyPair)pr.ReadObject();
            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)KeyPair.Private);

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();// cspParams);
            csp.ImportParameters(rsaParams);
            return csp;
        }

        /// <summary>
        /// Import OpenSSH PEM private key string into MS RSAParameters
        /// </summary>
        /// <param name="pem"></param>
        /// <returns></returns>
        public static RSAParameters ImportPrivateKey(string pem)
        {
                PemReader pr = new PemReader(new StringReader(pem));
                AsymmetricCipherKeyPair KeyPair = (AsymmetricCipherKeyPair)pr.ReadObject();
                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)KeyPair.Private);
                return rsaParams;
        }
 
        /// <summary>
        /// Import OpenSSH PEM public key string into MS RSACryptoServiceProvider
        /// </summary>
        /// <param name="pem"></param>
        /// <returns></returns>
        public static RSACryptoServiceProvider cImportPublicKey(string pem)
        {
            PemReader pr = new PemReader(new StringReader(pem));
            AsymmetricKeyParameter publicKey = (AsymmetricKeyParameter)pr.ReadObject();
            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)publicKey);

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();// cspParams);
            csp.ImportParameters(rsaParams);
            return csp;
        }


        /// <summary>
        /// Import OpenSSH PEM public key string into MS RSAParameters
        /// </summary>
        /// <param name="pem"></param>
        /// <returns></returns>
        public static RSAParameters ImportPublicKey(string pem)
        {
            PemReader pr = new PemReader(new StringReader(pem));
            AsymmetricKeyParameter publicKey = (AsymmetricKeyParameter)pr.ReadObject();
            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)publicKey);
            return rsaParams;
        }


        /// <summary>
        /// Export private (including public) key from MS RSACryptoServiceProvider into OpenSSH PEM string
        /// slightly modified from https://stackoverflow.com/a/23739932/2860309
        /// </summary>
        /// <param name="csp"></param>
        /// <returns></returns>
        public static string ExportPrivateKey(RSAParameters parameters,bool boundaries=false)
        {
            StringWriter outputStream = new StringWriter();

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
                    EncodeIntegerBigEndian(innerWriter, parameters.D);
                    EncodeIntegerBigEndian(innerWriter, parameters.P);
                    EncodeIntegerBigEndian(innerWriter, parameters.Q);
                    EncodeIntegerBigEndian(innerWriter, parameters.DP);
                    EncodeIntegerBigEndian(innerWriter, parameters.DQ);
                    EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                // WriteLine terminates with \r\n, we want only \n
                if (boundaries) outputStream.Write("-----BEGIN RSA PRIVATE KEY-----\n");
                // Output as Base64 with lines chopped at 64 characters
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.Write(base64, i, Math.Min(64, base64.Length - i));
                    outputStream.Write("\n");
                }
                if (boundaries) outputStream.Write("-----END RSA PRIVATE KEY-----");
            }

            return outputStream.ToString();
        }

        public static string ExportPrivateKey(RSACryptoServiceProvider csp, bool boundaries = false)
        {

            if (csp.PublicOnly) throw new ArgumentException("CSP does not contain a private key", "csp");
            var parameters = csp.ExportParameters(true);

            return ExportPrivateKey(parameters,boundaries);
        }
            /// <summary>
            /// Export public key from MS RSACryptoServiceProvider into OpenSSH PEM string
            /// slightly modified from https://stackoverflow.com/a/28407693
            /// </summary>
            /// <param name="csp"></param>
            /// <returns></returns>
            public static string ExportPublicKey(RSAParameters parameters,bool boundaries = false)
        {
            StringWriter outputStream = new StringWriter();
            
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    innerWriter.Write((byte)0x30); // SEQUENCE
                    EncodeLength(innerWriter, 13);
                    innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
                    var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
                    EncodeLength(innerWriter, rsaEncryptionOid.Length);
                    innerWriter.Write(rsaEncryptionOid);
                    innerWriter.Write((byte)0x05); // NULL
                    EncodeLength(innerWriter, 0);
                    innerWriter.Write((byte)0x03); // BIT STRING
                    using (var bitStringStream = new MemoryStream())
                    {
                        var bitStringWriter = new BinaryWriter(bitStringStream);
                        bitStringWriter.Write((byte)0x00); // # of unused bits
                        bitStringWriter.Write((byte)0x30); // SEQUENCE
                        using (var paramsStream = new MemoryStream())
                        {
                            var paramsWriter = new BinaryWriter(paramsStream);
                            EncodeIntegerBigEndian(paramsWriter, parameters.Modulus); // Modulus
                            EncodeIntegerBigEndian(paramsWriter, parameters.Exponent); // Exponent
                            var paramsLength = (int)paramsStream.Length;
                            EncodeLength(bitStringWriter, paramsLength);
                            bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
                        }
                        var bitStringLength = (int)bitStringStream.Length;
                        EncodeLength(innerWriter, bitStringLength);
                        innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
                    }
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                // WriteLine terminates with \r\n, we want only \n
                if (boundaries) outputStream.Write("-----BEGIN PUBLIC KEY-----\n");
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.Write(base64, i, Math.Min(64, base64.Length - i));
                    outputStream.Write("\n");
                }

                if (boundaries) outputStream.Write("-----END PUBLIC KEY-----");
            }

            return outputStream.ToString();
        }

        public static string ExportPublicKey(RSACryptoServiceProvider csp, bool boundaries = false)
        {
            var parameters = csp.ExportParameters(false);

            return ExportPublicKey(parameters,boundaries);
        }

        /// <summary>
        /// https://stackoverflow.com/a/23739932/2860309
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="length"></param>
        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }

        /// <summary>
        /// https://stackoverflow.com/a/23739932/2860309
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        /// <param name="forceUnsigned"></param>
        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }

        public RSAParameters CreatePublicKey(DerInteger modulus, DerInteger exponent)
        {
            RsaKeyParameters keyParameters = new RsaKeyParameters(false, modulus.PositiveValue, exponent.PositiveValue);
            return DotNetUtilities.ToRSAParameters(keyParameters); 
        }

        public RSAParameters CreatePublicKey(String pem)
        {
            Asn1Object obj = Asn1Object.FromByteArray(Convert.FromBase64String(pem));
            DerSequence publicKeySequence = (DerSequence)obj;

            DerBitString encodedPublicKey = (DerBitString)publicKeySequence[1];
            DerSequence publicKey = (DerSequence)Asn1Object.FromByteArray(encodedPublicKey.GetBytes());

            DerInteger modulus = (DerInteger) publicKey[0];
            DerInteger exponent = (DerInteger) publicKey[1];

            return CreatePublicKey(modulus, exponent);
        }
        /*
                public static string ExportDSAPrivateKey2(DSAParameters parameters, bool boundaries = false)
                {
                    StringWriter outputStream = new StringWriter();

                    using (var stream = new MemoryStream())
                    {
                        var writer = new BinaryWriter(stream);
                        writer.Write((byte)0x30); // SEQUENCE
                        using (var innerStream = new MemoryStream())
                        {
                            var innerWriter = new BinaryWriter(innerStream);
                            EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                            EncodeIntegerBigEndian(innerWriter, parameters.P);
                            EncodeIntegerBigEndian(innerWriter, parameters.Q);
                            EncodeIntegerBigEndian(innerWriter, parameters.G);
                            EncodeIntegerBigEndian(innerWriter, parameters.Y);
                            EncodeIntegerBigEndian(innerWriter, parameters.X);
                            var length = (int)innerStream.Length;
                            EncodeLength(writer, length);
                            writer.Write(innerStream.GetBuffer(), 0, length);
                        }

                        var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                        // WriteLine terminates with \r\n, we want only \n
                        if (boundaries) outputStream.Write("-----BEGIN DSA PRIVATE KEY-----\n");
                        // Output as Base64 with lines chopped at 64 characters
                        for (var i = 0; i < base64.Length; i += 64)
                        {
                            outputStream.Write(base64, i, Math.Min(64, base64.Length - i));
                            outputStream.Write("\n");
                        }
                        if (boundaries) outputStream.Write("-----END DSA PRIVATE KEY-----");
                    }

                    return outputStream.ToString();
                }
        */
        public static string ExportRSAKey(AsymmetricKeyParameter Key)
        {
            var stringWriter = new StringWriter();
            var pemWriter = new PemWriter(stringWriter);
            pemWriter.WriteObject(Key);
            stringWriter.Flush();
            stringWriter.Close();

            return stringWriter.ToString();
        }
        public static string ExportDSAPrivateKey(DSAParameters parameters)
        {
            IBigInteger DSAParaG = new IBigInteger(parameters.G);
            IBigInteger DSAParaP = new(1, parameters.P);
            IBigInteger DSAParaQ = new(1, parameters.Q);
            IBigInteger DsaPrivateX = new (1, parameters.X);

            DsaParameters para = new DsaParameters(DSAParaP, DSAParaQ, DSAParaG);
            DsaPrivateKeyParameters dsaPriv = new DsaPrivateKeyParameters(DsaPrivateX, para);

            TextWriter tw = new StringWriter();
            PemWriter pmw = new PemWriter(tw);
            pmw.WriteObject(dsaPriv);
            pmw.Writer.Flush();

            return tw.ToString();
        }
        public static string ExportDSAPublicKey(DSAParameters parameters)
        {

            IBigInteger DSAParaG = new IBigInteger(parameters.G);
            IBigInteger DSAParaP = new (1, parameters.P);
            IBigInteger DSAParaQ = new (1, parameters.Q);
            IBigInteger DSAPublicY = new(1, parameters.Y);

            DsaParameters para = new DsaParameters(DSAParaP, DSAParaQ, DSAParaG);
            DsaPublicKeyParameters dsaPub = new (DSAPublicY, para);

            TextWriter tw = new StringWriter();
            PemWriter pmw = new PemWriter(tw);
            pmw.WriteObject(dsaPub);
            pmw.Writer.Flush();

            //SubjectPublicKeyInfo subInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(dsaPub);
            //DsaKeyParameters testResult = (DsaKeyParameters)PublicKeyFactory.CreateKey(subInfo);
            //PrivateKeyInfo privInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(dsaPriv);

            return tw.ToString();
        }
        public static DSAParameters ImportDSAPrivateKey(string pem)
        {
            
            PemObject pemObject;

            using (StringReader rdr = new(pem))
            {
                //DSA dsa;
                PemReader pr = new(rdr);
                pemObject= pr.ReadPemObject();
                byte[] keyBytes = pemObject.Content;

                Asn1Sequence seq = (Asn1Sequence)Asn1Object.FromByteArray(keyBytes);
                if (seq.Count != 6)
                    throw new PemException("malformed sequence in DSA private key");

                //DerInteger v = (DerInteger)seq[0];
                DerInteger p = (DerInteger)seq[1];
                DerInteger q = (DerInteger)seq[2];
                DerInteger g = (DerInteger)seq[3];
                DerInteger y = (DerInteger)seq[4];
                DerInteger x = (DerInteger)seq[5];

                DSAParameters dp=new();

                dp.P = p.Value.ToByteArrayUnsigned();
                dp.Q = q.Value.ToByteArrayUnsigned();
                dp.G = g.Value.ToByteArrayUnsigned();
                dp.Y = y.Value.ToByteArrayUnsigned();
                dp.X = x.Value.ToByteArrayUnsigned();

                return dp;
            }
        }

        public static DSAParameters ImportDSAPublicKey(string pem)
        {
            //PemObject pemObject;

            using (StringReader rdr = new(pem))
            {
                PemReader pr = new PemReader(new StringReader(pem));
                DsaPublicKeyParameters publicKey = (DsaPublicKeyParameters)pr.ReadObject();
                
                DSAParameters dp = new();

                dp.P = publicKey.Parameters.P.ToByteArrayUnsigned();
                dp.Q = publicKey.Parameters.Q.ToByteArrayUnsigned();
                dp.G = publicKey.Parameters.G.ToByteArrayUnsigned();
                dp.Y = publicKey.Y.ToByteArrayUnsigned();

                if (publicKey.Parameters.ValidationParameters != null)
                {
                    dp.Counter = publicKey.Parameters.ValidationParameters.Counter;
                    dp.Seed = publicKey.Parameters.ValidationParameters.GetSeed();
                }

                return dp;
            }

        }
    

    }
}
