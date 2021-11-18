using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.WebMotors.Aplicacao;
using Teste.WebMotors.Aplicacao.Interfaces;
using Teste.WebMotors.Dominio.Interfaces.InterfaceRepositorio;
using Teste.WebMotors.Dominio.Interfaces.InterfaceServicos;
using Teste.WebMotors.Dominio.Servicos;
using Teste.WebMotors.Infraestrutura.Configuracoes;
using Teste.WebMotors.Infraestrutura.Repositorio;

namespace Teste.WebMotors.Api
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
            services.AddCors();
            services.AddDbContext<Contexto>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // INTERFACE E REPOSITORIO
            services.AddSingleton(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>));
            services.AddSingleton<IRepositorioAnuncio, RepositorioAnuncio>();

            // SERVI�O DOMINIO
            services.AddSingleton<IServicoAnuncio, ServicoAnuncio>();

            // INTERFACE APLICA��O
            services.AddSingleton<IAplicacaoAnuncio, AplicacaoAnuncio>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Teste.WebMotors.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste.WebMotors.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
