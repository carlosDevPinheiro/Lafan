using LF.SysAdm.API.Security;
using LF.SysAdm.Business;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Data.Repositorys.Dapper;
using LF.SysAdm.Data.Repositorys.EF;
using LF.SysAdm.Data.UOW;
using LF.SysAdm.Domain.Business;
using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Domain.Repositorys;
using LF.SysAdm.Domain.UOW;
using LF.SysAdm.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace LF.SysAdm.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }


        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6bbbb645024";
        private const string SECRET_KEY = "c1f51f42-5727-4d15-b787-c6bbbb645024";

        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));
        public void ConfigureServices(IServiceCollection services)
        {
            // adiciona o Mvc
            services.AddMvc(config => 
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()       // apartir daqui a aplicação requer usuario autenticado
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            
            services.AddCors(); // Libera requisições  localhost

            // depois de liberar requisições de todas urls  Cors  requisições externas 
            // adicionar nas as polices

            services.AddAuthorization(options =>
            {                 /* nome da policy*/                                       /* requerendo Claim*/
                options.AddPolicy("Users", policy => policy.RequireClaim("Lafan", "Commom","Admin"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("Lafan", "Admin"));

            });

            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = ISSUER;
                options.Audience = AUDIENCE;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });



            #region [Dapper]
            //services.AddScoped<IDbConnectionContext, DbContextDapper>();
            //services.AddTransient<IRepositoryUser, RepositoryUserDapper>();
            //services.AddTransient<IUnityOfWork, UnityOfWorkDapper>();
            //services.AddTransient<IRepositorySupply, RepositorySupplyDapper>();
            //services.AddTransient<IRepositoryCustomer, RepositoryCustomerDapper>();
            //services.AddTransient<IRepositoryAddress, RepositoryAddressDapper>();
            #endregion

            #region [Entity Framework]
            services.AddScoped<IDbConnectionContext, DbContextEF>();
            services.AddTransient<IRepositoryUser, RepositoryUserEF>();
            services.AddTransient<IUnityOfWork, UnityOfWorkEF>();
            services.AddTransient<IRepositorySupply, RepositorySupplyEF>();
            services.AddTransient<IRepositoryCustomer, RepositoryCustomerEF>();
            services.AddTransient<IRepositoryAddress, RepositoryAddressEF>();
            #endregion


            services.AddTransient<IBusinessUser, BusinessUser>();
            services.AddTransient<IBusinessSupply, BusinessSupply>();
            services.AddTransient<IBusinessCustomer, BusinessCustomer>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var tokenValidationParametersLafan = new TokenValidationParameters
            {
                ValidateIssuer = true, // validando o Issuer
                ValidIssuer = ISSUER,  // passando o Issuer

                ValidateAudience = true,  // validando o AUDIENCE
                ValidAudience = AUDIENCE, // passando o Audience

                // ================= Audience e Issuer são processos de validações de aplicação que são criadas nos provedores externo exemplo facebook necessita de criar uma app para social login.
                // autencação da aplicação e não do usuario

                ValidateIssuerSigningKey = true, // validando a chave simetrica
                IssuerSigningKey = _signingKey, // passando a chave simetrica

                RequireExpirationTime = true,  // requerendo tempo de expiração para o token
                ValidateLifetime = true,        // tempo que o token vai durar 

                ClockSkew = TimeSpan.Zero  // resolve problema de UTC horaios diverentes pelo fuso ex. Brasil e EUA 
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                // o AutomaticAuthenticate e AutomaticChallenge configruar a aplicação para authenticação somente do usuario, não precisando autenticar a aplicação para gerar o token
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParametersLafan
            });
          
            app.UseCors(x =>
            {
                x.AllowAnyHeader(); // permitir os headers 
                x.AllowAnyMethod(); // permitir os metodos 
                x.AllowAnyOrigin(); // permitir as Origens               
            });

            app.UseMvc();

            RunTime.ConnectionString = Configuration.GetConnectionString("Lafan");
           
        }
    }
}


// This method gets called by the runtime. Use this method to add services to the container.
// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.



//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello World!");
//});