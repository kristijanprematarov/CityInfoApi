using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CityInfo.API.Data;
using CityInfo.API.Repositories;
using CityInfo.API.Repositories.Interfaces;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region AddMvcWithJsonNamingPoliciesConfiguration
            //services.AddMvc()
            //    .AddJsonOptions(options =>
            //    {
            //        #region UpperCaseLetterNamingPolicy

            //        //return a json response with property keys default cased like in Model classes example => Id , instead of id
            //        //options.JsonSerializerOptions.PropertyNamingPolicy = null;

            //        #endregion
            //        #region camelCaseLetterNamingPolicy

            //        //for camel case(which is default simply erase this or add the following)
            //        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            //        #endregion
            //    });
            #endregion

            //get response in XML format
            //services.AddMvc()
            //    .AddMvcOptions(options =>
            //    {
            //        options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            //    });

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddXmlSerializerFormatters();

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

            var connectionString = _config.GetConnectionString("CityInfoConnectionString");
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            //REPOSITORIES
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();

            //AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
