using System;
using System.IO;
using System.Security.Cryptography;

namespace Ingress.DTOs
{
    public class LoginDTO
    {
        private static readonly byte[] _key = {243, 121, 48, 33, 161, 230, 184, 22, 197, 195, 53, 7, 60, 121, 203, 2, 210, 252, 84, 26, 130, 254, 225, 172, 99, 183, 50, 94, 247, 202, 138, 115};
        private static readonly byte[] _iv = {6, 111, 245, 105, 157, 240, 204, 12, 1, 151, 252, 229, 123, 122, 151, 15};

        public string Username { get; set; }
        public byte[] PasswordBytes { get; set; }

        public static byte[] Encrypt(string password)
        {
            if (password == null || password.Length <= 0)
                throw new ArgumentNullException(nameof(password));
            
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _iv;
                
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(password);
                    }

                    return msEncrypt.ToArray();
                }
            }
        }

        public static string Decrypt(byte[] cipherText)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _iv;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                    return srDecrypt.ReadToEnd();
            }
        }
    }
}