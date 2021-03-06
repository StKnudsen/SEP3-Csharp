using System.Linq;
using BusinessServer.Hubs;
using BusinessServer.Services;
using BusinessServer.Services.Admin;
using BusinessServer.Services.DNNRService;
using BusinessServer.Services.GroupService;
using BusinessServer.Services.Restaurateur;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusinessServer
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
            services.AddSingleton<IGroupService, GroupService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IDNNRService, DNNRService>();
            services.AddScoped<IRestaurateurService, RestaurateurService>();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] {"application/octet-stream"});
            });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<UserHub>("/userhub");
                endpoints.MapHub<GroupHub>("/grouphub");
                endpoints.MapHub<AdminHub>("/adminhub");
                endpoints.MapHub<DNNRHub>("/dnnrhub");
                endpoints.MapHub<RestaurateurHub>("/restauranthub");
            });
        }
    }
}