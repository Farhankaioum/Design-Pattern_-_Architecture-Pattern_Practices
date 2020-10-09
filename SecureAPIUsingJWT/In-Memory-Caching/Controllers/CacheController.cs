using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using In_Memory_Caching.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace In_Memory_Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;

        public CacheController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        [HttpGet("{key}")]
        public IActionResult GetCache(string key)
        {
            string value = string.Empty;
            memoryCache.TryGetValue(key, out value);
            return Ok(value);
        }

        [HttpPost]
        public IActionResult SetCache(CacheRequest data)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024
            };

            memoryCache.Set(data.key, data.value, cacheExpiryOptions);

            return Ok();
        }
    }
}
