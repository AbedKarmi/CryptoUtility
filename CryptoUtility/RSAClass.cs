using System;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace CryptoUtility;

public struct RSAKey
{
    public string P { get; set; }
    public string Q { get; set; }
    public string D { get; set; }
    public string DP { get; set; }
    public string DQ { get; set; }
    public string InverseQ { get; set; }
    public string Exponent { get; set; }
    public string Modulus { get; set; }
}

public class RSAClass
{
    private const int UTF8 = 65001;
    private RSAParameters privateKey;


    private RSAParameters publicKey;

    private RSACryptoServiceProvider rsa;

    /// <summary>
    ///     Create a new RSA with the specified key size, an error is thrown
    /// </summary>
    public RSAClass(int keySize = 2048)
    {
        CspParameters rsaParams = new();
        rsaParams.Flags = CspProviderFlags.UseMachineKeyStore;
        RSAObject = new RSACryptoServiceProvider(keySize, rsaParams);
    }


    /// <summary>
    ///     Create an RSA with the specified key. The xml can contain only one public key or private key, or both, and an error
    ///     will be thrown.
    /// </summary>
    
    public RSAClass(string xml)
    {
        var rsaParams = new CspParameters
        {
            Flags = CspProviderFlags.UseMachineKeyStore
        };
        RSAObject = new RSACryptoServiceProvider(rsaParams);
        rsa.FromXmlString(xml);
    }

    private static void DoNothing()
    {

    }
    /// <summary>
    ///     Create RSA from a pem file, pem is a public key or private key, and an error is thrown
    /// </summary>
    public RSAClass(string pemString, bool noop)
    {
        if (noop) DoNothing();
        RSAObject = PEM.FromPEM(pemString).GetRSA();
    }

    /// <summary>
    ///     Create RSA through a pem object, pem is a public key or private key, and an error is thrown
    /// </summary>
    public RSAClass(PEM pem)
    {
        RSAObject = pem.GetRSA();
    }

    /// <summary>
    ///     This method will first generate RSA_PEM and then create RSA: construct a PEM from the public key exponent and
    ///     private key exponent, and calculate P and Q in reverse, but they may be the same as those of the original generated
    ///     key.
    ///     Note: If the first byte of all parameters is 0, you must remove it first
    ///     Error will throw an exception
    /// </summary>
    /// <param name="modulus">must provide modulus</param>
    /// <param name="exponent">Public key exponent must be provided</param>
    /// <param name="dOrNull">The private key index may not be provided, and the exported PEM only contains the public key</param>
    public RSAClass(byte[] modulus, byte[] exponent, byte[] dOrNull)
    {
        RSAObject = new PEM(modulus, exponent, dOrNull).GetRSA();
    }

    /// <summary>
    ///     This method will first generate RSA_PEM and then create RSA: construct a PEM from the full amount of PEM field
    ///     data, except that modulus and public key exponent must be provided, all other private key exponent information is
    ///     either provided or not provided ( The exported PEM only contains the public key)
    ///     Note: If the first byte of all parameters is 0, you must remove it first
    /// </summary>
    public RSAClass(byte[] modulus, byte[] exponent, byte[] d, byte[] p, byte[] q, byte[] dp, byte[] dq,
        byte[] inverseQ)
    {
        RSAObject = new PEM(modulus, exponent, d, p, q, dp, dq, inverseQ).GetRSA();
    }

    /// <summary>
    ///     The lowest RSACryptoServiceProvider object
    /// </summary>
    public RSACryptoServiceProvider RSAObject
    {
        get => rsa;
        set
        {
            rsa = value;
            publicKey = rsa.ExportParameters(false);
            privateKey = rsa.ExportParameters(true);
        }
    }


    /// <summary>
    ///     Key bits
    /// </summary>
    public int KeySize => rsa.KeySize;

    /// <summary>
    ///     Whether to include the private key
    /// </summary>
    public bool HasPrivate => !rsa.PublicOnly;

    public static string Info =>
        @"RSA
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
";

    public static RSAKey RSAToKey64(RSAParameters key)
    {
        var key64 = new RSAKey(); //{ Section = section, Key = key, Value = value };
        if (key.P != null) key64.P = Convert.ToBase64String(key.P);
        if (key.Q != null) key64.Q = Convert.ToBase64String(key.Q);
        if (key.D != null) key64.D = Convert.ToBase64String(key.D);
        if (key.DP != null) key64.DP = Convert.ToBase64String(key.DP);
        if (key.DQ != null) key64.DQ = Convert.ToBase64String(key.DQ);
        if (key.InverseQ != null) key64.InverseQ = Convert.ToBase64String(key.InverseQ);
        if (key.Exponent != null) key64.Exponent = Convert.ToBase64String(key.Exponent);
        if (key.Modulus != null) key64.Modulus = Convert.ToBase64String(key.Modulus);
        return key64;
    }

    public static RSAParameters Key64ToRSA(RSAKey key64)
    {
        var key = new RSAParameters();

        if (!string.IsNullOrEmpty(key64.P)) key.P = Convert.FromBase64String(key64.P);
        if (!string.IsNullOrEmpty(key64.Q)) key.Q = Convert.FromBase64String(key64.Q);
        if (!string.IsNullOrEmpty(key64.D)) key.D = Convert.FromBase64String(key64.D);
        if (!string.IsNullOrEmpty(key64.DP)) key.DP = Convert.FromBase64String(key64.DP);
        if (!string.IsNullOrEmpty(key64.DQ)) key.DQ = Convert.FromBase64String(key64.DQ);
        if (!string.IsNullOrEmpty(key64.InverseQ)) key.InverseQ = Convert.FromBase64String(key64.InverseQ);
        if (!string.IsNullOrEmpty(key64.Exponent)) key.Exponent = Convert.FromBase64String(key64.Exponent);
        if (!string.IsNullOrEmpty(key64.Modulus)) key.Modulus = Convert.FromBase64String(key64.Modulus);
        return key;
    }

    public static void XMLWrite(RSAParameters key, string file)
    {
        var key64 = RSAToKey64(key);

        var mySerializer = new XmlSerializer(typeof(RSAKey));
        var myWriter = new StreamWriter(file);
        mySerializer.Serialize(myWriter, key64);
        myWriter.Close();
    }

    public static RSAParameters XMLRead(string file)
    {
        var mySerializer = new XmlSerializer(typeof(RSAKey));
        var myFileStream = new FileStream(file, FileMode.Open);

        var key64 = (RSAKey)mySerializer.Deserialize(myFileStream);
        myFileStream.Close();

        return Key64ToRSA(key64);
    }

    public void SetKey(RSAParameters RSAParameters_0, bool isPrivateKey)
    {
        if (isPrivateKey)
            privateKey = RSAParameters_0;
        else
            publicKey = RSAParameters_0;
    }

    public RSAParameters GetKey(bool isPrivateKey)
    {
        return isPrivateKey ? privateKey : publicKey;
    }

    public RSAParameters CreateKey(int keySize = 2048)
    {
        if (rsa != null)
            rsa.Dispose();

        rsa = new RSACryptoServiceProvider(keySize);
        {
            rsa.PersistKeyInCsp = false;

            publicKey = rsa.ExportParameters(false);
            privateKey = rsa.ExportParameters(true);

            return privateKey;
        }
    }

    public void SetPublicKey(byte[] publicKEY, byte[] exponent)
    {
        publicKey.Modulus = publicKEY;
        publicKey.Exponent = exponent;
        rsa.ImportParameters(publicKey);
    }

    public void SetKey(byte[] privateKEY, byte[] publicKEY, byte[] exponent)
    {
        privateKey.D = privateKEY;
        privateKey.Modulus = publicKEY;
        privateKey.Exponent = exponent;
        rsa.ImportParameters(privateKey);
    }

    public void SetKey(RSAParameters privateKEY)
    {
        privateKey = privateKEY;
        rsa.ImportParameters(privateKey);
    }

    /// <summary>
    ///     Export the key pair in XML format, if convertToPublic RSA with private key will only return public key, RSA with
    ///     only public key will not be affected
    /// </summary>
    public string ToXML(bool convertToPublic = false)
    {
        return rsa.ToXmlString(!rsa.PublicOnly && !convertToPublic);
    }

    /// <summary>
    ///     Export the key pair into a PEM object, if convertToPublic RSA with private key will only return public key, RSA
    ///     with only public key will not be affected
    /// </summary>
    public PEM ToPEM(bool convertToPublic = false)
    {
        return new PEM(rsa, convertToPublic);
    }

    public string PEMPKCS1(bool convertToPlublic = false)
    {
        return ToPEM(convertToPlublic).ToPEM_PKCS1();
    }

    public string PEMPKCS8(bool convertToPlublic = false)
    {
        return ToPEM(convertToPlublic).ToPEM_PKCS8();
    }

    /// <summary>
    ///     Encrypted string (utf-8), an error is thrown
    /// </summary>
    public string Encrypt(string str, int codePage = UTF8, bool usePadding = false)
    {
        var encoding = Encoding.GetEncoding(codePage);
        return Convert.ToBase64String(Encrypt(encoding.GetBytes(str), usePadding));
    }

    /// <summary>
    ///     Encrypted data, error throws an exception
    /// </summary>
    public byte[] Encrypt(byte[] data, Func<byte[], bool, byte[]> DoEncrypt, bool usePadding = false)
    {
        var blockLen = rsa.KeySize / 8 - 11;
        if (data.Length <= blockLen) return DoEncrypt(data, false);

        using var dataStream = new MemoryStream(data);
        using var enStream = new MemoryStream();
        var buffer = new byte[blockLen];
        var len = dataStream.Read(buffer, 0, blockLen);

        while (len > 0)
        {
            var block = new byte[len];
            Array.Copy(buffer, 0, block, 0, len);

            var enBlock = DoEncrypt(block, usePadding);
            enStream.Write(enBlock, 0, enBlock.Length);

            len = dataStream.Read(buffer, 0, blockLen);
        }

        return enStream.ToArray();
    }

    public byte[] Encrypt(byte[] data, bool usePadding = false)
    {
        return Encrypt(data, rsa.Encrypt, usePadding);
    }

    public byte[] Encrypt(byte[] data, bool usePrivate, bool usePadding = false)
    {
        if (usePrivate)
            return EncryptWithPrivate(data, usePadding);
        return Encrypt(data, usePadding);
    }

    public byte[] EncryptWithPrivate(byte[] data, bool usePadding = false)
    {
        using RSACryptoServiceProvider csp = new();

        csp.ImportParameters(privateKey);
        return Encrypt(data, csp.PrivateEncryption, usePadding);
    }

    /// <summary>
    ///     Decrypt the string (utf-8), return null if decryption is abnormal
    /// </summary>
    public string Decrypt(string str, int codePage = UTF8, bool usePadding = false)
    {
        if (string.IsNullOrEmpty(str)) return null;
        byte[] byts = null;
        try
        {
            byts = Convert.FromBase64String(str);
        }
        catch
        {
        }

        if (byts == null) return null;
        var val = Decrypt(byts, usePadding);
        if (val == null) return null;
        var encoding = Encoding.GetEncoding(codePage);
        return encoding.GetString(val);
    }

    /// <summary>
    ///     Decrypt the data, return null if decryption is abnormal
    /// </summary>
    public byte[] Decrypt(byte[] data, Func<byte[], bool, byte[]> DoDecrypt, bool usePadding = false)
    {
        try
        {
            var blockLen = rsa.KeySize / 8;
            if (data.Length <= blockLen) return DoDecrypt(data, usePadding);

            using var dataStream = new MemoryStream(data);
            using var deStream = new MemoryStream();
            var buffer = new byte[blockLen];
            var len = dataStream.Read(buffer, 0, blockLen);

            while (len > 0)
            {
                var block = new byte[len];
                Array.Copy(buffer, 0, block, 0, len);

                var deBlock = DoDecrypt(block, usePadding);
                deStream.Write(deBlock, 0, deBlock.Length);

                len = dataStream.Read(buffer, 0, blockLen);
            }

            return deStream.ToArray();
        }
        catch
        {
            return null;
        }
    }

    public byte[] Decrypt(byte[] data, bool usePadding = false)
    {
        return Decrypt(data, rsa.Decrypt, usePadding);
    }

    public byte[] Decrypt(byte[] data, bool usePrivate, bool usePadding = false)
    {
        if (usePrivate)
            return Decrypt(data, usePadding);
        return DecryptWithPublic(data, usePadding);
    }

    public byte[] DecryptWithPublic(byte[] data, bool usePadding = false)
    {
        using RSACryptoServiceProvider csp = new(); 

        csp.ImportParameters(publicKey);
        return Decrypt(data, csp.PublicDecryption, usePadding);
    }

    /// <summary>
    ///     Sign str and specify the hash algorithm (such as SHA256)
    /// </summary>
    public string Sign(string str, string hashAlgorithm = "SHA256", int codePage = UTF8)
    {
        var encoding = Encoding.GetEncoding(codePage);
        return Convert.ToBase64String(Sign(encoding.GetBytes(str), hashAlgorithm));
    }

    /// <summary>
    ///     Sign the data and specify the hash algorithm (such as SHA256)
    /// </summary>
    public byte[] Sign(byte[] data, string hashAlgorithm)
    {
        return rsa.SignData(data, hashAlgorithm);
    }

    public byte[] SignWithFormatter(byte[] hashOfDataToSign, string hashAlgorithm = "SHA256")
    {
        using var rsa = new RSACryptoServiceProvider();
        rsa.PersistKeyInCsp = false;
        rsa.ImportParameters(privateKey);

        var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
        rsaFormatter.SetHashAlgorithm(hashAlgorithm);

        return rsaFormatter.CreateSignature(hashOfDataToSign);
    }

    public bool VerifyWithFormatter(byte[] hashOfDataToSign, byte[] signature, string hashAlgorithm = "SHA256")
    {
        using var rsa = new RSACryptoServiceProvider();
        rsa.ImportParameters(publicKey);

        var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
        rsaDeformatter.SetHashAlgorithm(hashAlgorithm);

        return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
    }

    /// <summary>
    ///     Verify whether the signature of the string str is sgin, and specify the hash algorithm (such as SHA256)
    /// </summary>
    public bool Verify(string str, string signature, string hashAlgorithm = "SHA256", int codePage = UTF8)
    {
        byte[] byts = null;
        var encoding = Encoding.GetEncoding(codePage);

        try
        {
            byts = Convert.FromBase64String(signature);
        }
        catch
        {
        }

        if (byts == null) return false;
        return Verify(encoding.GetBytes(str), byts, hashAlgorithm);
    }

    /// <summary>
    ///     Verify that the signature of data is sgin, and specify the hash algorithm (such as SHA256)
    /// </summary>
    public bool Verify(byte[] data, byte[] signature, string hashAlgorithm = "SHA256")
    {
        try
        {
            return rsa.VerifyData(data, hashAlgorithm, signature);
        }
        catch
        {
            return false;
        }
    }
}

public static class RSAExtenstions
{
    public static byte[] PrivateEncryption(this RSACryptoServiceProvider rsa, byte[] data, bool usePadding = false)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        if (rsa.PublicOnly)
            throw new InvalidOperationException("Private key is not loaded");

        var maxDataLength = rsa.KeySize / 8 - 6;
        if (data.Length > maxDataLength)
            throw new ArgumentOutOfRangeException(nameof(data),
                string.Format(
                    "Maximum data length for the current key size ({0} bits) is {1} bytes (current length: {2} bytes)",
                    rsa.KeySize, maxDataLength, data.Length));

        // Add 4 byte padding to the data, and convert to BigInteger struct
        var numData = BigIntegerHelper.GetBig(usePadding ? AddPadding(data) : data);

        var rsaParams = rsa.ExportParameters(true);
        var D = BigIntegerHelper.GetBig(rsaParams.D);
        var Modulus = BigIntegerHelper.GetBig(rsaParams.Modulus);
        var encData = BigInteger.ModPow(numData, D, Modulus);

        return encData.ToByteArray();
    }

    public static byte[] PublicDecryption(this RSACryptoServiceProvider rsa, byte[] cipherData, bool usePadding = false)
    {
        if (cipherData == null)
            throw new ArgumentNullException(nameof(cipherData));

        var numEncData = new BigInteger(cipherData);

        var rsaParams = rsa.ExportParameters(false);
        var Exponent = BigIntegerHelper.GetBig(rsaParams.Exponent);
        var Modulus = BigIntegerHelper.GetBig(rsaParams.Modulus);

        var decData = BigInteger.ModPow(numEncData, Exponent, Modulus);

        var data = decData.ToByteArray();
        var result = new byte[data.Length];
        Array.Copy(data, result, result.Length);
        if (usePadding) result = RemovePadding(result);

        Array.Reverse(result);
        return result;
    }


    // Add 4 byte random padding, first bit *Always On*
    private static byte[] AddPadding(byte[] data)
    {
        var rnd = new Random();
        var paddings = new byte[4];
        rnd.NextBytes(paddings);
        paddings[0] = (byte)(paddings[0] | 128);

        var results = new byte[data.Length + 4];

        Array.Copy(paddings, results, 4);
        Array.Copy(data, 0, results, 4, data.Length);
        return results;
    }

    private static byte[] RemovePadding(byte[] data)
    {
        var results = new byte[data.Length - 4];
        Array.Copy(data, results, results.Length);
        return results;
    }
}