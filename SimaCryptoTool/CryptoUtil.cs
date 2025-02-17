﻿using System;
using System.ComponentModel.Design;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace SimaCryptoTool
{
    public class CryptoUtil
    {
        public static bool VerifyChallange(byte[] certificateData, byte[] challengeData, byte[] signature , string algorithm = "SHA-256withECDSA")
        {
            try
            {
                // Load the certificate
                X509CertificateParser certParser = new X509CertificateParser();
                X509Certificate certificate = certParser.ReadCertificate(certificateData);

                // Extract the public key from the certificate
                AsymmetricKeyParameter publicKey = certificate.GetPublicKey();

                // Initialize a signer using SHA-256 with ECDSA
                ISigner verifier = SignerUtilities.GetSigner(algorithm);

                // Initialize the verifier with the public key
                verifier.Init(false, publicKey);

                // Pass the challenge data (original message)
                verifier.BlockUpdate(challengeData, 0, challengeData.Length);

                // Verify the signature
                return verifier.VerifySignature(signature);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }


    }


    public class Certtest
    {

        public static void test()
        {
            string cert =
                @"MIIEMjCCA5SgAwIBAgIOK5N8S+cxh9MAAAA5x5YwCgYIKoZIzj0EAwMwfDELMAkGA1UEBhMCQVoxOzA5BgNVBAoTMk5hdGlvbmFsIENlcnRpZmljYXRlIFNlcnZpY2VzIENlbnRlciBvZiBBemVyYmFpamFuMTAwLgYDVQQDEydBemVyYmFpamFuIE5hdGlvbmFsIElzc3VpbmcgTW9iaWxlIENBMDEwHhcNMjQxMDIyMTE1NTE1WhcNMjcxMDIyMTE1NTE1WjBoMQswCQYDVQQGEwJBWjEkMCIGA1UEAwwbVMaPUkxBTiBVU1VCT1YgUkFTxLBNIE/EnkxVMQ8wDQYDVQQEEwZVU1VCT1YxEDAOBgNVBCoMB1TGj1JMQU4xEDAOBgNVBAUTBzZHVjhFVUswWTATBgcqhkjOPQIBBggqhkjOPQMBBwNCAAS4GW9frYw1OmuvK6KKTKUW5FX/W0m4zA0TkyR6BI/9oXjs2LFV3Vo274KzD+F38TiDO/otReu/uC5SgAF0U/dio4ICDDCCAggwDgYDVR0PAQH/BAQDAgbAMB0GA1UdDgQWBBSR4Bp8Uj7m2UCGOLmGQiGLlV6KaTAxBgNVHREEKjAohiZCX2JiZWJjMjQ1LTU4OWQtNDgwOC1hODA5LTY5NDdiMTdkZjQ5NTAfBgNVHSMEGDAWgBSNi2xCtuf4Ll3nn1rOaEvbO340gDA+BgNVHR8ENzA1MDOgMaAvhi1odHRwOi8vbW9iaWxlMi5lLWltemEuYXovY2RwYWlhL0FaTklNQ0EwMS5jcmwwdAYIKwYBBQUHAQEEaDBmMDkGCCsGAQUFBzAChi1odHRwOi8vbW9iaWxlMi5lLWltemEuYXovY2RwYWlhL0FaTklNQ0EwMS5jcnQwKQYIKwYBBQUHMAGGHWh0dHA6Ly9tb2JpbGUyLmUtaW16YS5hei9vY3NwMD0GCSsGAQQBgjcVBwQwMC4GJisGAQQBgjcVCIKtxhSBs4cRuZMNhuf5VIHtqQyBYYa2zm+BgItlAgFkAgEJMB8GA1UdJQQYMBYGCCsGAQUFBwMEBgorBgEEAYI3CgMMMCkGCSsGAQQBgjcVCgQcMBowCgYIKwYBBQUHAwQwDAYKKwYBBAGCNwoDDDBCBgNVHSAEOzA5MDcGCisGAQQBgoBLBwEwKTAnBggrBgEFBQcCARYbaHR0cHM6Ly9tb2JpbGUuYXovcmVwb3NpdHJ5MAoGCCqGSM49BAMDA4GLADCBhwJBEw02qxvRZ7spk0FGh+3e8oou7s71g8OCSSrFhsZQZeXaucaLxhQ/c24AkwfSnQhRH7WGKBsBS0BFyBwkVHtEf5gCQgDnaeiNKa+gSOuA/sbHv0ZGifQOz352oIeNtHtWtSKVROE5DtvhzvTWLT2ZiSGohgFb/LBacJj7Kabb71x9NaLtdw==";
            //string data = @"Mq==";
            string data = @"MzFiNGY3ZGItZWY3MS00MDNmLTkxNDgtNjg3MmNkMDcwYTAz";

            string signature =
                @"MEUCIElfH0ADF8y5ZoXgXLsURxsna6jcqQBwSezXNa6yoZz2AiEAzYg6VAd/M2Tf9X6plteI0p+MEkIXopivhxqsgSnjpQw=";


            var res = VerifySignature(Convert.FromBase64String(cert), Convert.FromBase64String(data),
                Convert.FromBase64String(signature));
        }


        public static bool VerifySignature(byte[] certificateData, byte[] challengeData, byte[] signature)
        {
            try
            {
                // Load the certificate
                X509CertificateParser certParser = new X509CertificateParser();
                X509Certificate certificate = certParser.ReadCertificate(certificateData);

                // Extract the public key from the certificate
                AsymmetricKeyParameter publicKey = certificate.GetPublicKey();

                // Initialize a signer using SHA-256 with ECDSA
                ISigner verifier = SignerUtilities.GetSigner("SHA-256withECDSA");

                // Initialize the verifier with the public key
                verifier.Init(false, publicKey);

                // Pass the challenge data (original message)
                verifier.BlockUpdate(challengeData, 0, challengeData.Length);

                // Verify the signature
                return verifier.VerifySignature(signature);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Verification failed: " + ex.Message);
                return false;
            }
        }

    }
}
