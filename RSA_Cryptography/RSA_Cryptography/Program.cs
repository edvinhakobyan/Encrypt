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
    public class Coding
    {
        RSAParameters rrr = new RSAParameters();

        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);

        private RSAParameters privateKey;
        private RSAParameters publicKey;

        public Coding()
        {
            privateKey = csp.ExportParameters(true);
            publicKey = csp.ExportParameters(false);
        }

        public string Encrypt(string Text)
        {
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(publicKey);

            byte[] TextBytes = Encoding.Unicode.GetBytes(Text);
            byte[] EncryptBytes = csp.Encrypt(TextBytes, false);

            return Convert.ToBase64String(EncryptBytes);
        }

        public string Decrypt(string EncryptText)
        {
            csp = new RSACryptoServiceProvider();
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



    class Program
    {
        static void Main(string[] args)
        {
            Coding R = new Coding();
            //Console.WriteLine($"PublicKey: \n {R.publicKeyToString()}");

            var Text = "BetConstruct";

            Console.WriteLine($"Text: \n {Text}");

            var EncryptText = R.Encrypt(Text);

            Console.WriteLine($"Encrypt Text: \n {EncryptText}");

            var DecryptTex = R.Decrypt(EncryptText);

            Console.WriteLine($"Decrypt Text: \n {DecryptTex}");
        }
    }
}
