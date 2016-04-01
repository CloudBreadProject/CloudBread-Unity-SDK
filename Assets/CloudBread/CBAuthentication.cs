using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace CloudBread
{
    public static class CBAuthentication
    {
        public static string AES_encrypt(string Input)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(CBSetting.aesKey);           //UTF8 값이기 때문에 1=49 로 처리됨. string을 byte로 변환해 넘길때 주의
                aes.IV = Encoding.UTF8.GetBytes(CBSetting.aesIV);
                var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] xBuff = null;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                    {
                        byte[] xXml = Encoding.UTF8.GetBytes(Input);
                        cs.Write(xXml, 0, xXml.Length);
                    }
                    xBuff = ms.ToArray();
                }
                string Output = Convert.ToBase64String(xBuff);
                return Output;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // 대칭키 AES256
        public static string AES_decrypt(string Input)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(CBSetting.aesKey);
                aes.IV = Encoding.UTF8.GetBytes(CBSetting.aesIV);
                var decrypt = aes.CreateDecryptor();
                byte[] xBuff = null;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                    {
                        byte[] xXml = Convert.FromBase64String(Input);
                        cs.Write(xXml, 0, xXml.Length);
                    }
                    xBuff = ms.ToArray();
                }
                string Output = Encoding.UTF8.GetString(xBuff);
                return Output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}