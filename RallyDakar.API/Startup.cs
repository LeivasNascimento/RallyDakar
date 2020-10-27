using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RallyDakar.Dominio.DbContexto;
using RallyDakar.Dominio.Interfaces;
using RallyDakar.Dominio.Repositorios;

namespace RallyDakar.API
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
            /* Basicamente cria o contexto da base na mem�ria com o nome informado;  
             * E o tempo de vida do contexto existir� durante o escopo da requisi��o = 
             * A APi � requisitada, e gerada um contexto e vai existir durante esta requis��o depois � finalizada
             * uma nova requisi��o feita, � gerada uma nova instancia do DBContext 
             */
            services.AddDbContext<RallyDbContexto>(opt => opt.UseInMemoryDatabase("RallyDb"), 
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped
                ); 

            services.AddControllers();
            services.AddScoped<IPilotoRepository, PilotoRepository>();
            // linha 42: A INJE��O DE DEPEND�NCIA NO CONSTRUTOR DO PilotoRepository J� � OBTIDA 
            // AO EXECUTAR O PROJETO DA API, DEVIDA LINHA 36, ONDE H� A INSTANCIA��O DA CLASSE RallyDbContexto
            // E AUTOMATICAMENTE NA LINHA 42, A INEJ��O DE DEPEND�NCIA IR� 'PASSAR' A INSTANCIA DA CLASSE
            // RallyDbContexto NO PARAMETRO DO CONSTRUTOR DE PilotoRepository (OU QUALQUER OUTRO REPOSITORY
            // ADICIONADO EM AddScoped EM ConfigureServices


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
