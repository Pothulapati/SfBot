using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Bot.Builder.Ai.LUIS;

namespace SfBot
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(env.ContentRootPath)
                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                         .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                         .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => Configuration);
            services.AddBot<SfBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);
                options.Middleware.Add(new ShowTypingMiddleware());
                options.Middleware.Add(new LuisRecognizerMiddleware(new LuisModel("50dfd4cb-7f19-499f-b9d3-a36bed6ffc62", "bc111f4506974c5b9980d58a625a70c6", new Uri("https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/"))));
            });

           //These are the services that will be available to the web app. as you can see we maded sfbot available, with some middleware that the message will go through,
           // first configuration, thenn typing which sends a typing activity to the user as soon as the message comes. after the middlewares it is sent to SFBOT.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Antha thope em kaadhu, we will do research and decide on one architecture, then will explain on a call. Ok. Discuss cheddham architecture kuda but finally the decision is yours as the CTO. :-)
                // Yeah I will make sure we decide on one nice arch based on resources and time we have :) 
                // Sure. Good Night!
                // Yeah Take care of
                // I will message you tomorrow morning... (y)
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseBotFramework();


        }
    }
}
