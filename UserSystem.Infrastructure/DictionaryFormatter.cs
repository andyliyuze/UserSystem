using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Infrastructure
{
   public class DictionaryFormatter
    {
        /// <summary>  
        ///   
        /// 将对象属性转换为key-value对  
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        public static Dictionary<String, String> MapToDictionary(Object o)
        {
            var map = new Dictionary<String, String>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                if (p.GetGetMethod() != null && p.GetGetMethod().IsPublic)
                {
                    map.Add(p.Name, p.GetValue(o).ToString());
                }
            }

            return map;
        }
    }
}
