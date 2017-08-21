using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using CityInfo.API.Services;

namespace CityInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));


            //.AddJsonOptions(o =>
            //{
            //    //if there's some Strategy for naming in Json
            //    if(o.SerializerSettings.ContractResolver != null)
            //    {
            //        //if so, then get Contract Resolver
            //        var castedResolver = o.SerializerSettings.ContractResolver
            //            as DefaultContractResolver;
            //        //and set it to NULL, so there will be no name resolving
            //        //and Member names wil be Serialized or Deserialized as the're written in the model
            //        castedResolver.NamingStrategy = null;
            //    }
            //});

            services.AddTransient<LocalMailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            //loggerFactory.AddProvider(new NLogLoggerProvider());
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            app.UseMvc();

            //app.Run((context) =>
            //{
            //    throw new Exception("Example Exception");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
