using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFF_Sqlite.Context;
using Microsoft.EntityFrameworkCore;
using SFF_Sqlite.Repository;

namespace SFF_Sqlite
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
            services.AddDbContext<MyDbContest>(o => o.UseSqlite("Data Source=minDatabas.db"));

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IFilmClubRepository, FilmClubRepository>();
            services.AddScoped<ILendingRepository, LendingRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ILabelRepository, LabelRepository>();

            // For XML
            services.AddControllers().AddXmlSerializerFormatters();

            services.AddControllers();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            });
        }

    }

}
