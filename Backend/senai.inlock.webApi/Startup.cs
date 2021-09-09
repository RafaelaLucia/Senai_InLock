using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";

            })
                 .AddJwtBearer("JwtBearer", options => {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         //validar quem esta emitindo o token
                         ValidateIssuer = true,
                         //validar quem esta recebendo o token
                         ValidateAudience = true,
                         //validar o tempo de expiracao
                         ValidateLifetime = true,
                         //definindo a chave de seguranca
                         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticacao")),
                         //tempo de expiracao do token
                         ClockSkew = TimeSpan.FromMinutes(30),
                         //define o nome de quem emite o token (issue)
                         ValidIssuer = "inlock.webApi",
                         //define o nome de quem recebe o token (audience)
                         ValidAudience = "inlock.webApi"
                     };
                 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
