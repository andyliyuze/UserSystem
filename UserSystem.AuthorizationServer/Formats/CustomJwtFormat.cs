using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
namespace UserSystem.AuthorizationServer.Formats
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string AudiencePropertyKey = "as:aud";

        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
           
            string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) 
                ? data.Properties.Dictionary[AudiencePropertyKey] : null;

            if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            //Audience audience = AudiencesStore.FindAudience(audienceId);

            //真是场景里，应该从Client表获取加密秘钥
            string symmetricKeyAsBase64 = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";
            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
          
            //生成JWT所需参数
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;
            var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByteArray), SecurityAlgorithms.HmacSha256Signature);
             
            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, SigningCredentials);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);
            var s = TimeSpan.FromMinutes(30);
            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}