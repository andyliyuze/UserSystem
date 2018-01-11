using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Infrastructure
{
    public class UrlChecker
    {
        public static bool CheckIsValid(string url, UriHostNameType type) 
        {
            Uri uri;
            var result = Uri.TryCreate(url, UriKind.Absolute, out uri);
            if (!result || uri.HostNameType != type || !uri.IsAbsoluteUri )
            {
                return false;
            }
            return true;
        }
    }
}
