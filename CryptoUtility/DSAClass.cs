using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
//using Chilkat;

namespace CryptoUtility
{
    // The value to hold the signed value.
    // byte[] SignedHashValue = DSASignHash(HashValue, privateKeyInfo, "SHA1");

    // Verify the hash and display the results.
    //bool verified = DSAVerifyHash(HashValue, SignedHashValue, publicKeyInfo, "SHA1");
    public class DSAKey
    {
        public string P { get; set; }
        public string Q { get; set; }
        public string G { get; set; }
        public string Y { get; set; }
        public string X { get; set; }
        public string J { get; set; }
        public string Seed { get; set; }
        public string Counter { get; set; }
    }

    public class DSAClass
    {

        public static string Info
        {
            get { return @"DSA 
---
1. Choose H algorithm (SHA-1, SHA-2, ...)
2. Choose KeyLength-1 L , DSS-KeyLength
3. Choose KeyLength-2 N , N<L and N<= Len(H)
4. Choose N-bit Prime q , called prime divisor
5. Choose L-bit Prime p , (p-1) mod q=0, called prime modulus
6. Choose random integer h from {2 .. p-2}
7. Compute g = h^((p-1)/q) mod p, if g=1, change h, commonly h=2
8. Choose randmon integer x, 0<x<q , private key (p,q,g,x)
9. Compute y=g^x mod p, public key (p,q,g,y)

Signing
-------
m : message to be signed 

1. Choose random integer k, 0<k<q
2. Compute r=(g^k mod p) mod q, if r=0 then change key
3. Compute s=(k^(-1) * (H(m)+x*r)) mod q, if s=0, change k, restart

Signature is (r,s)



Verifying
---------
1. Verify 0<r<q and 0<s<q
2. Compute w=s^(-1) mod q
3. Compute u1=H(m) * w mod q
4. Compute u2=r*w mod q
5. Compute v=(g^u1 * y^u2 mod p) mod q

if (v=r) then Verified";  }
        }


        private DSAParameters privateKey;
        private DSAParameters publicKey;

        public DSAKey DSAToXML(DSAParameters key)
        {
            DSAKey xml = new DSAKey(); //{ Section = section, Key = key, Value = value };
            if (key.P != null) xml.P = Convert.ToBase64String(key.P);
            if (key.Q != null) xml.Q = Convert.ToBase64String(key.Q);
            if (key.G != null) xml.G = Convert.ToBase64String(key.G);
            if (key.Y != null) xml.Y = Convert.ToBase64String(key.Y);
            if (key.X != null) xml.X = Convert.ToBase64String(key.X);
            if (key.J != null) xml.J = Convert.ToBase64String(key.J);
            if (key.Seed != null) xml.Seed = Convert.ToBase64String(key.Seed);
            xml.Counter = Convert.ToBase64String(BitConverter.GetBytes((Int16)key.Counter));
            return xml;
        }

        public DSAParameters XMLToDSA(DSAKey xml)
        {
            DSAParameters key = new DSAParameters();

            if (!String.IsNullOrEmpty(xml.P)) key.P = Convert.FromBase64String(xml.P);
            if (!String.IsNullOrEmpty(xml.Q)) key.Q = Convert.FromBase64String(xml.Q);
            if (!String.IsNullOrEmpty(xml.G)) key.G = Convert.FromBase64String(xml.G);
            if (!String.IsNullOrEmpty(xml.Y)) key.Y = Convert.FromBase64String(xml.Y);
            if (!String.IsNullOrEmpty(xml.X)) key.X = Convert.FromBase64String(xml.X);
            if (!String.IsNullOrEmpty(xml.J)) key.J = Convert.FromBase64String(xml.J);
            if (!String.IsNullOrEmpty(xml.Seed)) key.Seed = Convert.FromBase64String(xml.Seed);
            key.Counter = BitConverter.ToInt16(Convert.FromBase64String(xml.Counter), 0);
            return key;
        }
        public void XMLWrite(DSAParameters key, string file)
        {
            DSAKey xml = DSAToXML(key);

            XmlSerializer mySerializer = new XmlSerializer(typeof(DSAKey));
            StreamWriter myWriter = new StreamWriter(file);
            mySerializer.Serialize(myWriter, xml);
            myWriter.Close();
        }


        public DSAParameters XMLRead(string file)
        {
            DSAKey xml = new DSAKey();
            DSAParameters key;

            XmlSerializer mySerializer = new XmlSerializer(typeof(DSAKey));
            FileStream myFileStream = new FileStream(file, FileMode.Open);

            xml = (DSAKey)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();

            key = XMLToDSA(xml);

            return key;

        }

        public DSAParameters CreateKey(int keySize)
        {
            using (DSACryptoServiceProvider DSA = new DSACryptoServiceProvider(keySize))
            {
                privateKey = DSA.ExportParameters(true);
                publicKey = DSA.ExportParameters(false);
                
                return privateKey;
            }

        }

        public void SetKey(DSAParameters DSAParameters_0, bool isPrivateKey)
        {
            if (isPrivateKey) privateKey = DSAParameters_0; else publicKey = DSAParameters_0;
        }

        public DSAParameters GetKey(bool isPrivateKey)
        {
            return (isPrivateKey ? privateKey : publicKey);
        }

        public string Sign(string text)
        {

            byte[] bytes = Encoding.ASCII.GetBytes(text);
            string @string = "";
            try
            {
                using (SHA1 sha = new SHA1CryptoServiceProvider())
                {
                    byte[] rgbHash = sha.ComputeHash(bytes);
                    using (DSACryptoServiceProvider dsacryptoServiceProvider = new DSACryptoServiceProvider())
                    {
                        AsymmetricSignatureFormatter asymmetricSignatureformatter = new DSASignatureFormatter(dsacryptoServiceProvider);
                        dsacryptoServiceProvider.ImportParameters(privateKey);
                        @string = Convert.ToBase64String(asymmetricSignatureformatter.CreateSignature(rgbHash));
                    }
                }
            }
            catch { MessageBox.Show("Could not sign"); };
            return @string;
        }

        public bool isSignatureOk(string text, string signature)
        {
            bool ok = false;
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                byte[] rgbHash = sha.ComputeHash(bytes);
                using (DSACryptoServiceProvider dsacryptoServiceProvider = new DSACryptoServiceProvider())
                {
                    dsacryptoServiceProvider.ImportParameters(publicKey);

                    AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = new DSASignatureDeformatter(dsacryptoServiceProvider);
                    byte[] rgbSignature = Convert.FromBase64String(signature);
                    ok = asymmetricSignatureDeformatter.VerifySignature(rgbHash, rgbSignature);
                }
            }
            return ok;
        }

        public byte[] SignData(byte[] HashToSign, string HashAlg)
        {
            byte[] sig = null;

            try
            {
                // Create a new instance of DSACryptoServiceProvider.
                using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
                {
                    // Import the key information.
                    dsa.ImportParameters(privateKey);

                    // Create an DSASignatureFormatter object and pass it the
                    // DSACryptoServiceProvider to transfer the private key.
                    DSASignatureFormatter DSAFormatter = new DSASignatureFormatter(dsa);

                    // Set the hash algorithm to the passed value.
                    DSAFormatter.SetHashAlgorithm(HashAlg);

                    // Create a signature for HashValue and return it.
                    sig = DSAFormatter.CreateSignature(HashToSign);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }

            return sig;
        }

        public bool VerifySignature(byte[] HashValue, byte[] SignedHashValue, string HashAlg)
        {
            bool verified = false;

            try
            {
                // Create a new instance of DSACryptoServiceProvider.
                using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
                {
                    // Import the key information.
                    dsa.ImportParameters(publicKey);

                    // Create an DSASignatureDeformatter object and pass it the
                    // DSACryptoServiceProvider to transfer the private key.
                    DSASignatureDeformatter DSADeformatter = new DSASignatureDeformatter(dsa);

                    // Set the hash algorithm to the passed value.
                    DSADeformatter.SetHashAlgorithm(HashAlg);

                    // Verify signature and return the result.
                    verified = DSADeformatter.VerifySignature(HashValue, SignedHashValue);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }

            return verified;
        }
    }
}