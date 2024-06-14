using LicenseManagementSystem.DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace LicenseManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            Bootstrapper.Initialize(builder.Services);
            var app = builder.Build();

            app.UseCors(a => a
                      .SetIsOriginAllowed((host) => true)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
              );

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();



            app.MapControllers();

            app.Run();
        }
    }
}
