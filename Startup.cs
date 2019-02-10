using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiUsuarios.Models;
using ApiUsuarios.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace ApiUsuarios {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<MyDbContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection")));

            services.AddTransient<IUsuarioRepository, UsuarioRepository> ();

            //especifica o esquema usado para autenticacao do tipo Bearer
            // e 
            //define configurações como chave,algoritmo,validade, data expiracao...
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "toshiossada.com",
                    ValidAudience = "toshiossada.com",
                    IssuerSigningKey = new SymmetricSecurityKey (
                    Encoding.UTF8.GetBytes (Configuration["SecurityKey"]))
                    };

                    options.Events = new JwtBearerEvents {
                        OnAuthenticationFailed = context => {
                                Console.WriteLine ("Token inválido..:. " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context => {
                                Console.WriteLine ("Toekn válido...: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                    };
                });

            //Definindo Swagger
            services.AddSwaggerGen (
                c => {
                    c.SwaggerDoc ("v1", new Info {
                            Title = "UsuarioWebAPI",
                            Version = "v1", 
                            Description = "API usando DotNet Core + EntityFramework Core e JWT",
                            TermsOfService = "None",
                            Contact = new Contact { Name = "Toshi Ossada", Email = "toshiossada@gmail.com", Url = "https://github.com/toshiossada" },
                            License = new License { Name = "GNU", Url = "https://www.gnu.org/licenses/licenses.pt-br.html"}

                    });
                }
            );

            services.AddMvc ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseAuthentication ();
            //Definindo Swagger
            app.UseSwagger ();
            app.UseSwaggerUI (
                c => {
                    c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Minha API V1");
                }
            );

            app.UseMvc ();
        }
    }
}