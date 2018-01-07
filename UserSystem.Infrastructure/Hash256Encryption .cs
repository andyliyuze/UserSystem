

using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
//using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Infrastructure
{
   public class Hash256Encryption
    {
        public string Create(string value)
        {
            var a = Encoding.Default.GetBytes(value);

            string symmetricKeyAsBase64 = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";
            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
           

            byte[] byteArray22 = System.Text.Encoding.Default.GetBytes("{\"typ\": \"JWT\",\"alg\":\"HS256\"}");
            var cc = TextEncodings.Base64Url.Encode(byteArray22);
            var sss = Decode("eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImQ5Y2M2NTI5LTdmNDYtNGM4OS1hODgxLTMyZmRkMjczNWU0OCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJMaXl1emUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL2FjY2Vzc2NvbnRyb2xzZXJ2aWNlLzIwMTAvMDcvY2xhaW1zL2lkZW50aXR5cHJvdmlkZXIiOiJBU1AuTkVUIElkZW50aXR5IiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiJjMDJiNzY2YS05NTdiLTQ4NDItODUwYS01OTUxNTk3NWVmMjYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiSWRcIjpcImQ5Y2M2NTI5LTdmNDYtNGM4OS1hODgxLTMyZmRkMjczNWU0OFwiLFwiVXNlck5hbWVcIjpcIkxpeXV6ZVwiLFwiQmlydGhEYXlcIjpcIjE5OTMtMTAtMjNUMDA6MDA6MDBcIn0iLCJuYmYiOjE1MTUzMjE0NDgsImV4cCI6MTUxNTMyMzI0OCwiaXNzIjoiaHR0cDovL2p3dGF1dGh6c3J2LmF6dXJld2Vic2l0ZXMubmV0IiwiYXVkIjoiRmlzaCJ9");

            byte[] byteArray = System.Text.Encoding.Default.GetBytes("Hello");
            string str = System.Text.Encoding.Default.GetString(byteArray);
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] secretKeyByteArray = new byte[32]; //1byte=8bit 256 bit
                cryptoProvider.GetBytes(secretKeyByteArray);
                var s = byteToHexStr(secretKeyByteArray);
                var l= s.Length;
            }
            string hash = Effortless.Net.Encryption.Hash
                .Create(Effortless.Net.Encryption.HashType.SHA256, "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJmcm9tX3VzZXIiOiJCIiwidGFyZ2V0X3VzZXIiOiJBIn0", "mystar", false);
            return hash;

        }


        public string byteToHexStr(byte[] bytes)
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
        public   string Decode(string secureUrlBase64)
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
