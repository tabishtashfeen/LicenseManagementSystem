using FluentValidation.AspNetCore;
using LicenseManagementSystem.DataAccess.Context;
using LicenseManagementSystem.Helper.Mapping;
using LicenseManagementSystem.Helper.Validators;

namespace LicenseManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            Bootstrapper.Initialize(builder.Services);
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DatabaseContext>();
                    context.Database.Migrate(); // Apply pending migrations
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

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
