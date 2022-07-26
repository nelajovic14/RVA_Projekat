
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RVA_Projekat.Dogadjaji;
using RVA_Projekat.Infrastructure;
using RVA_Projekat.Initilaizer;
using RVA_Projekat.Interface;
using RVA_Projekat.Interface.Bruto;
using RVA_Projekat.Interface.InterfaceUser;
using RVA_Projekat.Interface.InterfaceZaposlenih;
using RVA_Projekat.Interface.Neto;
using RVA_Projekat.Mapping;
using RVA_Projekat.Repository;
using RVA_Projekat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVA_Projekat
{
    public class Startup
    {
        private readonly string _cors = "cors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<HonorarDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HonorarDataBase")));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RVA_Projekat", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Pleas enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:44386",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                };
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder => {
                    builder.WithOrigins("http://localhost:3000")//Ovde navodimo koje sve aplikacije smeju kontaktirati nasu,u ovom slucaju nas Angular front
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("user", policy => policy.RequireClaim("user")); //Ovde mozemo kreirati pravilo za validaciju nekog naseg claima
            });

            services.AddScoped<IUserInitializer, UserInitializer>();
            services.AddScoped<INetohonorarInitializer, NetohonorarInitializer>();
            services.AddScoped<IBrutoHonorarInitializer, BrutoHonorarInitializer>();
            services.AddScoped<IZaposleniInitializer, ZaposleniInitializer>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INetohonorarService, NetohonorarService>();
            services.AddScoped<IBrutoHonorarService, BrutoHonorarService>();
            services.AddScoped<IZaposleniService, ZapolseniService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INetohonorarRepository, NetohonorarRepository>();
            services.AddScoped<IBrutoHonorarRepository, BrutoHonorarRepository>();
            services.AddScoped<IZaposleniRepository, ZaposleniRepository>();

            services.AddScoped<IPorezRepository, PorezRepository>();
            services.AddSingleton<ILoggerManager, LoggerManager>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RVA_Projekat v1");
                   
                });

            }
       

            using (var scope = app.ApplicationServices.CreateScope())
            {
                // koristi se za inicijalizaciju podataka
                scope.ServiceProvider.GetRequiredService<IUserInitializer>().InitializeUseres();
                scope.ServiceProvider.GetRequiredService<IBrutoHonorarInitializer>().InitializBrutoHonorars();
                scope.ServiceProvider.GetRequiredService<INetohonorarInitializer>().InitializeNetohonorars();
                scope.ServiceProvider.GetRequiredService<IZaposleniInitializer>().InitializeUseres();
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(_cors);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
