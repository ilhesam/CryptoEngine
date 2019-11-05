using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace CryptoEngine
{
    public interface ICrypto
    {
        string Encrypt(string input, string key);
        string Decrypt(string input, string key);
    }

    public class Crypto : ICrypto
    {
        public string Encrypt([NotNull]string input, [NotNull] string key)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if(key == null) throw new ArgumentNullException(nameof(key));

            var inputArray = Encoding.UTF8.GetBytes(input);
            var keyArray = Encoding.UTF8.GetBytes(key);

            var tripleDES = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cryptoTransform = tripleDES.CreateEncryptor();

            var resultArray = cryptoTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);

            tripleDES.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt([NotNull] string input, [NotNull]string key)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (key == null) throw new ArgumentNullException(nameof(key));

            byte[] inputArray;

            try
            {
                inputArray = Convert.FromBase64String(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var keyArray = Encoding.UTF8.GetBytes(key);

            var tripleDES = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cryptoTransform = tripleDES.CreateDecryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
