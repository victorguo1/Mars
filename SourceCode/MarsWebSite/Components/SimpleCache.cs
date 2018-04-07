using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsWebSite.Components
{
    public static class SimpleCache
    {
        private static Dictionary<string, CacheObject> _simpleCache;
        static SimpleCache() {
            _simpleCache = new Dictionary<string, CacheObject>();
        }
        public static void Add(string key, int time, object value) {
            _simpleCache[key] = new CacheObject(key, DateTime.Now.AddMinutes(time), value);
        }

        public static object Get(string key){
            object value = null;
            if (_simpleCache[key] != null && _simpleCache[key].Expire < DateTime.Now ) {
                value = _simpleCache[key].Value;
            }

            return value;
        }
    }
}
