using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddMvc();
            //for api
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1",
               new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "TweetBook API", Version = "v1" });
            });
        }
    }
}
