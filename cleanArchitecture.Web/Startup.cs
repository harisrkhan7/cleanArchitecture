using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using cleanArchitecture.Infra;
using cleanArchitecture.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace cleanArchitecture.Web
{
    public class Startup
    {
        private const string swaggerVersion = "v3";
        private const string swaggerTitle = "Product API";
        private const string swaggerDocName = "v3";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerDocName, new Microsoft.OpenApi.Models.OpenApiInfo { Title = swaggerTitle, Version = swaggerVersion });
            });
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.ConfigureInfrastructure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            string swaggerEndPoint = $"../swagger/{swaggerDocName}/swagger.json";
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint(swaggerEndPoint, swaggerTitle);
                c.RoutePrefix = string.Empty;
            });

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
