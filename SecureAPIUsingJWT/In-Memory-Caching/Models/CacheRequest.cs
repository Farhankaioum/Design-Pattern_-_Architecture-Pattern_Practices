using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace In_Memory_Caching.Models
{
    public class CacheRequest
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
