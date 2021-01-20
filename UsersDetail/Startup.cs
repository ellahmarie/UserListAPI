using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UsersDetailAPI.IRepository;
using UsersDetailAPI.Models;
using UsersDetailAPI.Repository;

namespace UsersDetailAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.SetIsOriginAllowed((host) => true).WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddDbContext<UsersDetailContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("UsersDetailConnectionString"));
            });
            services.AddControllers();
            services.AddScoped<IUsersDetailsRepository, UsersDetailRepository>();
            services.AddScoped<IEmailRepository, SendEmailRepository>();
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
