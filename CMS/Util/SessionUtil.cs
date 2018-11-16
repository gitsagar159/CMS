using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CMS.Util
{
    public static class SessionUtil
    {
        //public static void SetObject<T>(this Session session, string key, T value)
        //{
        //    session.SetString(key, JsonConvert.SerializeObject(value));
        //}

        //public static T GetObject<T>(this Session session, string key)
        //{
        //    var value = session.GetString(key);
        //    return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        //}

        public static void SetObject<T>(string Kye, T value)
        {
            HttpContext.Current.Session["key"] = "aaa";

        }

        public static void GetObject<T>(string Kye,T value)
        {

        }

    }
}
