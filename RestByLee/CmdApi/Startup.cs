using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmdApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CmdApi
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
        }
         public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<CommandContext>(c =>
                c.UseSqlServer(_config.GetConnectionString("CommandContext"))); 
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
