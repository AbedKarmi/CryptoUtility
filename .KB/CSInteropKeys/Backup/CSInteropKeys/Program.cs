using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

// No Can Do: BigInteger is not Compliant
// [assembly: CLSCompliant(true)]
namespace CSInteropKeys
{
  class Program
  {
    internal static void Main(/* string[] args */)
    {
      TestRsaKeys();

      TestDsaKeys();
    }

    private static void TestDsaKeys()
    {
      CreateDsaKeys();

      LoadDsaPrivateKey();

      LoadDsaPublicKey();
    }

    private static void TestRsaKeys()
    {
      CreateRsaKeys();

      LoadRsaPrivateKey();

      LoadRsaPublicKey();
    }

    private static void CreateRsaKeys()
    {
      CspParameters csp = new CspParameters();

      csp.KeyContainerName = "RSA Test (OK to Delete)";

      const int PROV_RSA_FULL = 1;
      csp.ProviderType = PROV_RSA_FULL;

      const int AT_KEYEXCHANGE = 1;
      // const int AT_SIGNATURE = 2;
      csp.KeyNumber = AT_KEYEXCHANGE;

      RSACryptoServiceProvider rsa =
          new RSACryptoServiceProvider(1024, csp);
      rsa.PersistKeyInCsp = false;

      // Encoded key
      AsnKeyBuilder.AsnMessage key = null;

      // Private Key
      RSAParameters privateKey = rsa.ExportParameters(true);
      key = AsnKeyBuilder.PrivateKeyToPKCS8(privateKey);

      using (BinaryWriter writer = new BinaryWriter(
          new FileStream("private.rsa.cs.ber", FileMode.Create,
              FileAccess.ReadWrite)))
      {
        writer.Write(key.GetBytes());
      }

      // Public Key
      RSAParameters publicKey = rsa.ExportParameters(false);
      key = AsnKeyBuilder.PublicKeyToX509(publicKey);

      using (BinaryWriter writer = new BinaryWriter(
          new FileStream("public.rsa.cs.ber", FileMode.Create,
              FileAccess.ReadWrite)))
      {
        writer.Write(key.GetBytes());
      }

      // See http://blogs.msdn.com/tess/archive/2007/10/31/
      //   asp-net-crash-system-security-cryptography-cryptographicexception.aspx
      rsa.Clear();
    }

    private static void CreateDsaKeys()
    {
      CspParameters csp = new CspParameters();

      csp.KeyContainerName = "DSA Test (OK to Delete)";

      const int PROV_DSS_DH = 13;
      csp.ProviderType = PROV_DSS_DH;

      // Can't use AT_EXCHANGE for creation. This is
      //  a signature algorithm
      const int AT_SIGNATURE = 2;
      csp.KeyNumber = AT_SIGNATURE;

      DSACryptoServiceProvider dsa =
          new DSACryptoServiceProvider(1024, csp);
      dsa.PersistKeyInCsp = false;

      // Encoded key
      AsnKeyBuilder.AsnMessage key = null;

      // Private Key
      DSAParameters privateKey = dsa.ExportParameters(true);
      key = AsnKeyBuilder.PrivateKeyToPKCS8(privateKey);

      using (BinaryWriter writer = new BinaryWriter(
          new FileStream("private.dsa.cs.ber", FileMode.Create,
              FileAccess.ReadWrite)))
      {
        writer.Write(key.GetBytes());
      }

      // Public Key
      DSAParameters publicKey = dsa.ExportParameters(false);
      key = AsnKeyBuilder.PublicKeyToX509(publicKey);

      using (BinaryWriter writer = new BinaryWriter(
          new FileStream("public.dsa.cs.ber", FileMode.Create,
              FileAccess.ReadWrite)))
      {
        writer.Write(key.GetBytes());
      }

      // See http://blogs.msdn.com/tess/archive/2007/10/31/
      //   asp-net-crash-system-security-cryptography-cryptographicexception.aspx
      dsa.Clear();
    }

    private static void LoadDsaPrivateKey()
    {
      //
      // Load the Private Key
      //   PKCS#8 Format
      //
      AsnKeyParser keyParser =
        new AsnKeyParser("private.dsa.cs.ber");

      DSAParameters privateKey = keyParser.ParseDSAPrivateKey();

      //
      // Initailize the CSP
      //   Supresses creation of a new key
      //
      CspParameters csp = new CspParameters();
      csp.KeyContainerName = "DSA Test (OK to Delete)";

      // Can't use PROV_DSS_DH for loading. We have lost
      //   parameters such as seed and j.
      // const int PROV_DSS_DH = 13;
      const int PROV_DSS = 3;
      csp.ProviderType = PROV_DSS;

      // const int AT_EXCHANGE = 1;
      const int AT_SIGNATURE = 2;
      csp.KeyNumber = AT_SIGNATURE;

      //
      // Initialize the Provider
      //
      DSACryptoServiceProvider dsa =
        new DSACryptoServiceProvider(csp);
      dsa.PersistKeyInCsp = false;

      //
      // The moment of truth...
      //
      dsa.ImportParameters(privateKey);

      // See http://blogs.msdn.com/tess/archive/2007/10/31/
      //   asp-net-crash-system-security-cryptography-cryptographicexception.aspx
      dsa.Clear();
    }

    private static void LoadDsaPublicKey()
    {
      //
      // Load the Public Key
      //   X.509 Format
      //
      AsnKeyParser keyParser =
        new AsnKeyParser("public.dsa.cs.ber");

      DSAParameters publicKey = keyParser.ParseDSAPublicKey();

      //
      // Initailize the CSP
      //   Supresses creation of a new key
      //
      CspParameters csp = new CspParameters();

      // const int PROV_DSS_DH = 13;
      const int PROV_DSS = 3;
      csp.ProviderType = PROV_DSS;

      const int AT_SIGNATURE = 2;
      csp.KeyNumber = AT_SIGNATURE;

      csp.KeyContainerName = "DSA Test (OK to Delete)";

      //
      // Initialize the Provider
      //
      DSACryptoServiceProvider dsa =
        new DSACryptoServiceProvider(csp);
      dsa.PersistKeyInCsp = false;

      //
      // The moment of truth...
      //
      dsa.ImportParameters(publicKey);

      // See http://blogs.msdn.com/tess/archive/2007/10/31/
      //   asp-net-crash-system-security-cryptography-cryptographicexception.aspx
      dsa.Clear();
    }

    private static void LoadRsaPrivateKey()
    {
      //
      // Load the Private Key
      //   PKCS#8 Format
      //
      AsnKeyParser keyParser =
        new AsnKeyParser("private.rsa.cs.ber");

      RSAParameters privateKey = keyParser.ParseRSAPrivateKey();

      //
      // Initailize the CSP
      //   Supresses creation of a new key
      //
      CspParameters csp = new CspParameters();
      csp.KeyContainerName = "RSA Test (OK to Delete)";

      const int PROV_RSA_FULL = 1;
      csp.ProviderType = PROV_RSA_FULL;

      const int AT_KEYEXCHANGE = 1;
      // const int AT_SIGNATURE = 2;
      csp.KeyNumber = AT_KEYEXCHANGE;

      //
      // Initialize the Provider
      //
      RSACryptoServiceProvider rsa =
        new RSACryptoServiceProvider(csp);
      rsa.PersistKeyInCsp = false;

      //
      // The moment of truth...
      //
      rsa.ImportParameters(privateKey);

      // See http://blogs.msdn.com/tess/archive/2007/10/31/
      //   asp-net-crash-system-security-cryptography-cryptographicexception.aspx
      rsa.Clear();
    }

    private static void LoadRsaPublicKey()
    {
      //
      // Load the Public Key
      //   X.509 Format
      //
      AsnKeyParser keyParser =
        new AsnKeyParser("public.rsa.cs.ber");

      RSAParameters publicKey = keyParser.ParseRSAPublicKey();

      //
      // Initailize the CSP
      //   Supresses creation of a new key
      //
      CspParameters csp = new CspParameters();
      csp.KeyContainerName = "RSA Test (OK to Delete)";

      const int PROV_RSA_FULL = 1;
      csp.ProviderType = PROV_RSA_FULL;

      const int AT_KEYEXCHANGE = 1;
      // const int AT_SIGNATURE = 2;
      csp.KeyNumber = AT_KEYEXCHANGE;

      //
      // Initialize the Provider
      //
      RSACryptoServiceProvider rsa =
        new RSACryptoServiceProvider(csp);
      rsa.PersistKeyInCsp = false;

      //
      // The moment of truth...
      //
      rsa.ImportParameters(publicKey);

      // See http://blogs.msdn.com/tess/archive/2007/10/31/
      //   asp-net-crash-system-security-cryptography-cryptographicexception.aspx
      rsa.Clear();
    }
  }
}