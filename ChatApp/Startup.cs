using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.Data;
using ChatApp.Hubs;
using ChatApp.Mapper;
using ChatApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void AddMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void AddReposirories(IServiceCollection services)
        {
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient(typeof(IMessageRepository), typeof(MessageRepository));
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IMessageService), typeof(MessageService));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChatDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ChatDbContext")));

            services.AddCors(options=> options.AddPolicy("CorsPolicy",
                builder=> {
                    builder
                    .AllowAnyMethod()                    
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200");
                }));

            AddMapper(services);

            AddReposirories(services);

            AddServices(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:4201",
                    ValidateAudience = true,
                    ValidAudience = "http://localhost:4200",
                    RequireExpirationTime = true,
                    ValidateLifetime = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSignalR(options=> {
                options.EnableDetailedErrors = true;
            });

            services.AddControllers();            
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");            

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/messagehub");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Resource Server has been started successfully");
            });
        }
    }
}
