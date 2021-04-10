using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CryptoUtility
{        
        public class RSAKey
        {
            public String P { get; set; }
            public String Q { get; set; }
            public String D { get; set; }
            public String DP { get; set; }
            public String DQ { get; set; }
            public String InverseQ { get; set; }
            public String Exponent { get; set; }
            public String Modulus { get; set; }
        }
    public class RSAClass
    {

        public static string Info
        {
            get { return @"RSA
---
1. Choose two distinct prime numbers p, q-1
2. Compute n=p * q , modulus , its length is the key length
3. Compute Lambda(n), Carmichael's totient function
	- since n=pq, lambda(n)=LCM(lambda(p),lambda(q))
	- since p,q are prime, lambda(p)=phi(p)=p-1,likewise q
	- hence lambda(n)=LCM(p-1,q-1), keep secret
	- LCM using Euclidean algorithm, lcm(a,b)=abs(ab)/gcd(a,b)
4. Choose integer e, 1<e<lambda(n), gcd(e,lambda(n))=1, [coprime]
	- use short bit-length and small hamming weight
	- e is public exponent , commonly 65537, 2^16 + 1
5. Determine d, d ≡ e^(−1) (mod lambda(n)) , [congruous not equal]
	- d*e ≡ 1 (mod(lamda(n)), using Extended Euclidean algorithm
	- since e and lambda(n) coprime, a form of Bézout's identity
	- (d*e) mod lambda(n) =1
	- d is kept secret, private exponent

Note: Euler totient function phi(n)=(p-1)*(q-1) is used instead of lambda(n)
	  since phi(n) is always divisable by lambda(n), it also works
	 
Encryption:
c = m^e mod n

Decyption:
m = c^d mod n

Signing:
1. Compute h=hash(m)
2. Compute s=h^d mod n, Signature

Verifying:
1. Compute h=hash(m)
2. Compute v=s^e mod n
3. Compare v and h, if equal verified
"; }
        }
        private RSAParameters publicKey;
        private RSAParameters privateKey;

        public RSAKey RSAToXML(RSAParameters key)
        {
            RSAKey xml = new RSAKey(); //{ Section = section, Key = key, Value = value };
            if (key.P != null) xml.P = Convert.ToBase64String(key.P);
            if (key.Q != null) xml.Q = Convert.ToBase64String(key.Q);
            if (key.D != null) xml.D = Convert.ToBase64String(key.D);
            if (key.DP != null) xml.DP = Convert.ToBase64String(key.DP);
            if (key.DQ != null) xml.DQ = Convert.ToBase64String(key.DQ);
            if (key.InverseQ != null) xml.InverseQ = Convert.ToBase64String(key.InverseQ);
            if (key.Exponent != null) xml.Exponent = Convert.ToBase64String(key.Exponent);
            if (key.Modulus != null) xml.Modulus = Convert.ToBase64String(key.Modulus);
            return xml;
        }

        public RSAParameters XMLToRSA(RSAKey xml)
        {
            RSAParameters key = new RSAParameters();

            if (!String.IsNullOrEmpty(xml.P)) key.P = Convert.FromBase64String(xml.P);
            if (!String.IsNullOrEmpty(xml.Q)) key.Q = Convert.FromBase64String(xml.Q);
            if (!String.IsNullOrEmpty(xml.D)) key.D = Convert.FromBase64String(xml.D);
            if (!String.IsNullOrEmpty(xml.DP)) key.DP = Convert.FromBase64String(xml.DP);
            if (!String.IsNullOrEmpty(xml.DQ)) key.DQ = Convert.FromBase64String(xml.DQ);
            if (!String.IsNullOrEmpty(xml.InverseQ)) key.InverseQ = Convert.FromBase64String(xml.InverseQ);
            if (!String.IsNullOrEmpty(xml.Exponent)) key.Exponent = Convert.FromBase64String(xml.Exponent);
            if (!String.IsNullOrEmpty(xml.Modulus)) key.Modulus = Convert.FromBase64String(xml.Modulus);
            return key;
        }
        public void XMLWrite(RSAParameters key, string file)
        {
            RSAKey xml = RSAToXML(key);

            XmlSerializer mySerializer = new XmlSerializer(typeof(RSAKey));
            StreamWriter myWriter = new StreamWriter(file);
            mySerializer.Serialize(myWriter, xml);
            myWriter.Close();
        }


        public RSAParameters XMLRead(string file)
        {
            RSAKey xml = new RSAKey();
            RSAParameters key;

            XmlSerializer mySerializer = new XmlSerializer(typeof(RSAKey));
            FileStream myFileStream = new FileStream(file, FileMode.Open);

            xml = (RSAKey)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();

            key = XMLToRSA(xml);

            return key;
        }

        public void SetKey(RSAParameters RSAParameters_0, bool isPrivateKey)
        {
            if (isPrivateKey) privateKey = RSAParameters_0; else publicKey = RSAParameters_0;
        }

        public RSAParameters GetKey(bool isPrivateKey)
        {
            return (isPrivateKey ? privateKey : publicKey);
        }
        public RSAParameters CreateKey(int keySize=2048)
        {
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                rsa.PersistKeyInCsp = false;
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);

                return privateKey;
            }
        }

        public byte[] SignData(byte[] hashOfDataToSign,String hashAlgorithm="SHA256",int keySize=2048)
        {
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);

                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm(hashAlgorithm);

                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }

        public bool VerifySignature(byte[] hashOfDataToSign, byte[] signature, String hashAlgorithm = "SHA256", int keySize=2048)
        {
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                rsa.ImportParameters(publicKey);

                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm(hashAlgorithm);

                return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
            }
        }

        public void SetPublicKey(byte[] publicKEY,byte[] exponent)
        {
            publicKey.Modulus = publicKEY;
            publicKey.Exponent = exponent;
        }
        public void SetKey(byte[] privateKEY,byte[] publicKEY, byte[] exponent)
        {
            privateKey.D = privateKEY;
            privateKey.Modulus = publicKEY;
            privateKey.Exponent = exponent;
        }
        public void SetKey(RSAParameters privateKEY)
        {
            privateKey = privateKEY;
        }

        public byte[] Encrypt(byte[] data,bool usePrivateKey=true,bool usePadding=false)
        {
            using RSACryptoServiceProvider csp = new ();
            if (usePrivateKey)
            {
                csp.ImportParameters(privateKey);
                return csp.PrivareEncryption(data,usePadding);
            }
            else
            {
                csp.ImportParameters(publicKey);
                return csp.Encrypt(data, usePadding);
            }
        }

        public byte[] Decrypt(byte[] data, bool usePrivateKey = true,bool usePadding=false)
        {
            using RSACryptoServiceProvider csp = new();

            if (usePrivateKey)
            {
                csp.ImportParameters(privateKey);
                return csp.Decrypt(data, usePadding);
                
            }
            else
            {
                csp.ImportParameters(publicKey);
                return csp.PublicDecryption(data,usePadding);
            }
        }
    }

    public static class RSAExtenstions
    {
        public static byte[] PrivareEncryption(this RSACryptoServiceProvider rsa, byte[] data,bool usePadding=false)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (rsa.PublicOnly)
                throw new InvalidOperationException("Private key is not loaded");

            int maxDataLength = (rsa.KeySize / 8) - 6;
            if (data.Length > maxDataLength)
                throw new ArgumentOutOfRangeException("data", string.Format("Maximum data length for the current key size ({0} bits) is {1} bytes (current length: {2} bytes)", rsa.KeySize, maxDataLength, data.Length));

            // Add 4 byte padding to the data, and convert to BigInteger struct
            BigInteger numData = BigIntegerHelper.GetBig(usePadding?AddPadding(data):data);

            RSAParameters rsaParams = rsa.ExportParameters(true);
            BigInteger D = BigIntegerHelper.GetBig(rsaParams.D);
            BigInteger Modulus = BigIntegerHelper.GetBig(rsaParams.Modulus);
            BigInteger encData = BigInteger.ModPow(numData, D, Modulus);

            return encData.ToByteArray();
        }

        public static byte[] PublicDecryption(this RSACryptoServiceProvider rsa, byte[] cipherData, bool usePadding = false)
        {
            if (cipherData == null)
                throw new ArgumentNullException("cipherData");

            BigInteger numEncData = new BigInteger(cipherData);

            RSAParameters rsaParams = rsa.ExportParameters(false);
            BigInteger Exponent = BigIntegerHelper.GetBig(rsaParams.Exponent);
            BigInteger Modulus = BigIntegerHelper.GetBig(rsaParams.Modulus);

            BigInteger decData = BigInteger.ModPow(numEncData, Exponent, Modulus);

            byte[] data = decData.ToByteArray();
            byte[] result = new byte[data.Length];
            Array.Copy(data, result, result.Length);
            if (usePadding) result = RemovePadding(result);

            Array.Reverse(result);
            return result;
        }


        // Add 4 byte random padding, first bit *Always On*
        private static byte[] AddPadding(byte[] data)
        {
            Random rnd = new Random();
            byte[] paddings = new byte[4];
            rnd.NextBytes(paddings);
            paddings[0] = (byte)(paddings[0] | 128);

            byte[] results = new byte[data.Length + 4];

            Array.Copy(paddings, results, 4);
            Array.Copy(data, 0, results, 4, data.Length);
            return results;
        }

        private static byte[] RemovePadding(byte[] data)
        {
            byte[] results = new byte[data.Length - 4];
            Array.Copy(data, results, results.Length);
            return results;
        }
    }
}
