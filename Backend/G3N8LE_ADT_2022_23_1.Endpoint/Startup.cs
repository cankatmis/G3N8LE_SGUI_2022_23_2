using G3N8LE_ADT_2022_23_1.Data;
using G3N8LE_ADT_2022_23_1.Logic;
using G3N8LE_ADT_2022_23_1.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ////services.AddControllersWithViews().AddNewtonsoftJson(options =>
            ////options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddTransient<IStudentsLogic, StudentsLogic>();
            services.AddTransient<ITeachersLogic, TeachersLogic>();
            services.AddTransient<IReservationsLogic, ReservationsLogic>();
            services.AddTransient<IReservationsServicesLogic, ReservationsServicesLogic>();
            services.AddTransient<IClassesLogic, ClassesLogic>();
            services.AddTransient<IStudentsRepository, StudentsRepository>();
            services.AddTransient<ITeachersRepository, TeachersRepository>();
            services.AddTransient<IReservationsRepository, ReservationsRepository>();
            services.AddTransient<IReservationsServicesRepository, ReservationsServicesRepository>();
            services.AddTransient<IClassesRepository, ClassesRepository>();
            services.AddTransient<XYZDbContext, XYZDbContext>();
            services.AddSignalR();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:23079"));

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
