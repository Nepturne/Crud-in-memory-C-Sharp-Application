using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

namespace Shop
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
                services.AddControllers();
                // E Adiciona essa linha:
                services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
                // Dependency Injection - Injeção de Dependência:
                // Os nossos controllers dependem de DataContext, como o
                // DataContext chegará ao controller?  Não é problema do
                // controller. Isto é Injeção de Dependência.

                // Analogicamente quando voce vai trabalhar: você precisa
                // de uma mesa, de uma cadeira, de um computador, quem
                // vai providenciar isto para você não temos obrigação de
                // saber. Só sabemos que precisamos e isto é uma dependência
                // que se tem para começar os trabalhos.

                // No modelo de api , cada requisição que é feita na api,
                // é executada e depois o usuário é desligado/desconectado
                // da api.

                // Sendo assim quando fazemos uma requisição pra api,
                // a api vai fazer uma requisição pro banco e depois que
                // pedimos a execução do banco , a aplicação tem que fechar
                // a conexão com o banco, retornar os dados para a api.A api
                // retorna os dados pro usuário e fechar a conexão.

                // SQL Server tem um limite de conexões. Então é necessário
                // fechar as conexões.

                // Iremos configurar para que a api feche as conexões
                // automaticamente para economizar recursos com a linha:
                services.AddScoped<DataContext, DataContext>();
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                // Verifica se estamos em ambiente [enviroment] de Desenvolvimento
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                // Força a aplicação a utilizar Https
                app.UseHttpsRedirection();
                // Roteamento do ASP.NET MVC
                app.UseRouting();

                app.UseAuthorization();

                // Mapeamento das nossas URLS:
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }