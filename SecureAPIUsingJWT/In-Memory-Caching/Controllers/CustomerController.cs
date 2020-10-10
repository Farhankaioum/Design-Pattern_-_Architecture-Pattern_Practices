using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using In_Memory_Caching.Data;
using In_Memory_Caching.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace In_Memory_Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly ApplicationDbContext context;
        private readonly IDistributedCache distributedCache;

        public CustomerController(
            IMemoryCache memoryCache,
            ApplicationDbContext context,
            IDistributedCache distributed)
        {
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributed;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "customerList";

            if (!memoryCache.TryGetValue(cacheKey, out List<Customer> customerList))
            {
                customerList = await context.Customers.ToListAsync();

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                memoryCache.Set(cacheKey, customerList, cacheExpiryOptions);
            }

            return Ok(customerList);
        }

        // caching using Redis
        // step
        // Redis download & install
        // ./redis-server --port 4455/any-port : chage default port using powershell
        // .redis-cli -p 4455 : run redis cli with new port using another powershell window
        // Install-Package Microsoft.Extensions.Caching.StackExchangeRedis - install nuget package
        // services.AddStackExchangeRedisCache(options =>{ options.Configuration = "localhost:4455";}); // add this line in startup
        // IDistributedCache inject this interface
        // Do SetCache first serialize this data/object then convert into byte array, then create a DistributedCacheEntryOptions()
        // object with SetAbsoluteExpiration & SetSlidingExpiration chaining then setcache
        // reverse system for getCahce

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomerUsingRedisCache()
        {
            var cacheKey = "customerListRedis";
            string serializedCustomerList;

            var customerList = new List<Customer>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);

            if(redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<Customer>>(serializedCustomerList);
            }
            else
            {
                customerList = await context.Customers.ToListAsync();
                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }

            return Ok(customerList);
        }
    }
}
