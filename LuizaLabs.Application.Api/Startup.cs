using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LuizaLabs.Domain.Entities;
using LuizaLabs.Domain.Service;
using LuizaLabs.Infra.Cross;
using LuizaLabs.Infra.Data.Connection;
using LuizaLabs.Infra.Data.Interfaces;
using LuizaLabs.Infra.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LuizaLabs.Application.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
            {
                return new ConnectionMongo(Configuration.GetConnectionString("MongoConnection"));
            });

            services.AddSingleton(x =>
            {
                return Configuration;
            });

            services.AddScoped(x => x.GetService<ConnectionMongo>().GetMongoDatabase(Configuration.GetSection("AppSettings:MongoDataBase").Value));

            services.AddScoped<IRepUser, RepUser>();
            services.AddScoped<ISrvUser, SrvUser>();
            services.AddScoped<IRepProduct, RepProduct>();
            services.AddScoped<ISrvProduct, SrvProduct>();
            services.AddScoped<IRepWish, RepWish>();
            services.AddScoped<ISrvWish, SrvWish>();
            services.AddScoped<ISrvAuthentication, SrvAuthentication>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:SecretKeyAuth").Value))
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    string guidError = Guid.NewGuid().ToString();

                    string mensagemErro = $"Erro no processamento. Por favor, entre em contato com com suporte. Erro={guidError}";


                    if (error != null)
                    {
                        Exception ex = error.Error;

                        while (ex.InnerException != null)
                            ex = ex.InnerException;

                        switch (ex)
                        {
                            case ValidationException e:
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                mensagemErro = ex.Message;
                                break;
                            case AlreadyExistException e:
                                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                mensagemErro = ex.Message;
                                break;
                            case NotFoundException e:
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                mensagemErro = ex.Message;
                                break;
                            case NotContentException e:
                                context.Response.StatusCode = (int)HttpStatusCode.NoContent;
                                break;
                        }

                        
                        
                    }

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new{ Erro = mensagemErro }));
                });
            });


            app.UseAuthentication();
            app.UseMvc();
            
        }
    }
}
