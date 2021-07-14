namespace DisneyApi.Core.Api
{
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using DisneyApi.Core.Models.Context;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

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
            services.AddCors();
            services.AddMvc();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

           // services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddDbContext<DisneyDBContext>(cfg =>
            {
                //cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));   Produccion
                cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));     //  Base Test 
            });

            //services.AddTransient<IMailService, MailService>();
            services.AddScoped<PersonajeRepository>();
            services.AddScoped<PeliculaSerieRepository>();
            services.AddScoped<UsuarioRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DisneyApi.Core.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DisneyApi.Core.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
