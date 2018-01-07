using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace UserSystem.Infrastructure
{
    public class Hash256Encryption
    {
        public string Create(string value)
        {
            var enscrypt = ConfigurationManager.AppSettings["Sha256EncryptKey"].ToString();
           
            string hash = Effortless.Net.Encryption.Hash
                .Create(Effortless.Net.Encryption.HashType.SHA256,value , enscrypt, false);
            return hash;

        }

        public static string Create()
        {
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] secretKeyByteArray = new byte[32]; //1byte=8bit 256 bit
                cryptoProvider.GetBytes(secretKeyByteArray);
                var base64String = ByteToHexStr(secretKeyByteArray);

                var enscrypt = ConfigurationManager.AppSettings["Sha256EncryptKey"].ToString();

                string hash = Effortless.Net.Encryption.Hash
                .Create(Effortless.Net.Encryption.HashType.SHA256, base64String, enscrypt, false);

                return hash;
            }
        }

        //转为16进制的字符串
        static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        //将base64解码成可理解字符
        public string Decode(string secureUrlBase64)
        {
            secureUrlBase64 = secureUrlBase64.Replace('-', '+').Replace('_', '/');
            switch (secureUrlBase64.Length % 4)
            {
                case 2:
                    secureUrlBase64 += "==";
                    break;
                case 3:
                    secureUrlBase64 += "=";
                    break;
            }
            var bytes = Convert.FromBase64String(secureUrlBase64);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
