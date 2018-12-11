using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAutoface.Is;
using WebAutoface.Middleware;
using WebAutoface.Model;

namespace WebAutoface
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            services.AddOptions();

            services.Configure<SecretSettings>(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IPersonRepository, PersonRepository>();

            services.Add(ServiceDescriptor.Singleton(typeof
                (IPersonRepository), typeof(PersonRepository)));

            services.Add(ServiceDescriptor.Singleton(typeof
                (IPersonRepository), typeof(PersonRepository)));
            //var builder = new ContainerBuilder();//实例化 AutoFac  容器            
            //builder.Populate(services);
            //ApplicationContainer = builder.Build();


            services.AddSingleton<IPersonRepository>(new PersonRepository());//这里可以自定义传参
            services.AddSingleton<IPersonRepository>(pri => new PersonRepository());


            //return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器


            //services.AddAutofac();

           



        }

        // This method gets called by the runtime. Use this method to configure the HTTP reque st pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHelloWorld();

            app.UseMvc();
        }
    }
}
