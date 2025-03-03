using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IKE.LAST.HelperClass
{
    public static class EncryptionHelper
    {
        
        public static string EncryptToBase64(string plainText, string key)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = new byte[16]; // IV sıfırdan oluşturulabilir veya rastgele atanabilir.

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DecryptFromBase64(string cipherText, string key)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = new byte[16]; // Aynı IV kullanılmalı.

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static string EncryptLongToBase64(long value, string key)
        {
            // long değerini string'e dönüştür.
            string plainText = value.ToString();

            // Şifreleme işlemini uygula.
            return EncryptToBase64(plainText, key);
        }

        public static long DecryptBase64ToLong(string cipherText, string key)
        {
            // Şifre çözme işlemini uygula.
            string plainText = DecryptFromBase64(cipherText, key);

            // Şifre çözülmüş string'i long'a dönüştür.
            return long.Parse(plainText);
        }


        public static string EncryptWithIV(string plainText, string key)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.GenerateIV(); // Rastgele IV oluştur
                string ivBase64 = Convert.ToBase64String(aes.IV);

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    string cipherTextBase64 = Convert.ToBase64String(ms.ToArray());
                    return ivBase64 + ":" + cipherTextBase64; // IV'yi şifreli metinle birleştir
                }
            }
        }
    }
}
