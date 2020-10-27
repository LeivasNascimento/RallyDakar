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
            /* Basicamente cria o contexto da base na memória com o nome informado;  
             * E o tempo de vida do contexto existirá durante o escopo da requisição = 
             * A APi é requisitada, e gerada um contexto e vai existir durante esta requisção depois é finalizada
             * uma nova requisição feita, é gerada uma nova instancia do DBContext 
             */
            services.AddDbContext<RallyDbContexto>(opt => opt.UseInMemoryDatabase("RallyDb"), 
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped
                ); 

            services.AddControllers();
            services.AddScoped<IPilotoRepository, PilotoRepository>();
            // linha 42: A INJEÇÃO DE DEPENDÊNCIA NO CONSTRUTOR DO PilotoRepository JÁ É OBTIDA 
            // AO EXECUTAR O PROJETO DA API, DEVIDA LINHA 36, ONDE HÁ A INSTANCIAÇÃO DA CLASSE RallyDbContexto
            // E AUTOMATICAMENTE NA LINHA 42, A INEJÇÃO DE DEPENDÊNCIA IRÁ 'PASSAR' A INSTANCIA DA CLASSE
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
