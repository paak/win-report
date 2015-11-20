using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WINConnect.Libs.Crypto
{
    /// <summary>
    /// WIN Cryptography
    /// </summary>
    public static class WINCrypto
    {
        private static string DEFAULT_SEED = "thePassword";
        private static byte[] DEFAULT_SALT = Encoding.UTF8.GetBytes("saltIsGoodForYou");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        private static byte[] GetSalt(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
            {
                return DEFAULT_SALT;
            }
            return Encoding.UTF8.GetBytes(raw);
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            return Encrypt(input, string.Empty);
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="str"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Encrypt(string input, string salt)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            byte[] utfdata = UTF8Encoding.UTF8.GetBytes(input);

            // Our symmetric encryption algorithm
            AesManaged aes = new AesManaged();

            // We're using the PBKDF2 standard for password-based key generation
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(DEFAULT_SEED, GetSalt(salt));

            // Setting our parameters
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;

            aes.Key = rfc.GetBytes(aes.KeySize / 8);
            aes.IV = rfc.GetBytes(aes.BlockSize / 8);

            // Encryption
            ICryptoTransform encryptTransf = aes.CreateEncryptor();

            // Output stream, can be also a FileStream
            MemoryStream encryptStream = new MemoryStream();
            CryptoStream encryptor = new CryptoStream(encryptStream, encryptTransf, CryptoStreamMode.Write);

            encryptor.Write(utfdata, 0, utfdata.Length);
            encryptor.Flush();
            encryptor.Close();

            // Showing our encrypted content
            byte[] encryptBytes = encryptStream.ToArray();
            string encryptedString = Convert.ToBase64String(encryptBytes);

            input = encryptedString;
            return input;
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="base64Input"></param>
        /// <returns></returns>
        public static string Decrypt(string base64Input)
        {
            return Decrypt(base64Input, string.Empty);
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="str"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Decrypt(string base64Input, string salt)
        {
            if (string.IsNullOrWhiteSpace(base64Input))
            {
                return string.Empty;
            }

            base64Input = base64Input.Replace(" ", "+");
            byte[] encryptBytes = Convert.FromBase64String(base64Input);

            // Our symmetric encryption algorithm
            AesManaged aes = new AesManaged();

            // We're using the PBKDF2 standard for password-based key generation
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(DEFAULT_SEED, GetSalt(salt));

            // Setting our parameters
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;

            aes.Key = rfc.GetBytes(aes.KeySize / 8);
            aes.IV = rfc.GetBytes(aes.BlockSize / 8);

            // Now, decryption
            ICryptoTransform decryptTrans = aes.CreateDecryptor();

            // Output stream, can be also a FileStream
            MemoryStream decryptStream = new MemoryStream();
            CryptoStream decryptor = new CryptoStream(decryptStream, decryptTrans, CryptoStreamMode.Write);

            decryptor.Write(encryptBytes, 0, encryptBytes.Length);
            decryptor.Flush();
            decryptor.Close();

            // Showing our decrypted content
            byte[] decryptBytes = decryptStream.ToArray();
            string decryptedString = UTF8Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);

            return decryptedString;
        }
    }
}
