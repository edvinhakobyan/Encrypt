using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RSA_Cryptography
{
    public class RSA
    {
        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(1024);

        private RSAParameters privateKey;
        private RSAParameters publicKey;

        public RSA()
        {
            privateKey = csp.ExportParameters(true);
            publicKey = csp.ExportParameters(false);
        }

        public string Encrypt(string Text)
        {
            csp.ImportParameters(publicKey);

            byte[] TextBytes = Encoding.Unicode.GetBytes(Text);
            byte[] EncryptBytes = csp.Encrypt(TextBytes, false);

            return Convert.ToBase64String(EncryptBytes);
        }

        public string Decrypt(string EncryptText)
        {
            csp.ImportParameters(privateKey);

            byte[] EncryptTextBytes = Convert.FromBase64String(EncryptText);
            byte[] DecryptTextBytes = csp.Decrypt(EncryptTextBytes, false);

            return Encoding.Unicode.GetString(DecryptTextBytes);
        }

        public string publicKeyToString()
        {
            return JsonConvert.SerializeObject(publicKey);
        }
    }
    public class SHA
    {

        public string Encrypt(string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }


        private  string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            RSA R = new RSA();

            //Console.WriteLine($"PublicKey: {R.publicKeyToString()}");

            var Text = "BetConstruct";

            Console.WriteLine($"Text: {Text}");

            var EncryptText = R.Encrypt(Text);

            Console.WriteLine($"Encrypt Text: {EncryptText}");

            var DecryptTex = R.Decrypt(EncryptText);

            Console.WriteLine($"Decrypt Text: {DecryptTex}");

            Console.Read();
        }
    }
}
