
using Microsoft.EntityFrameworkCore;
using MyBoards.Entities;

namespace MyBoards
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MyBoardsContext>(
                    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnectionString"))
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MyBoardsContext>();
           
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if(pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }


            app.UseAuthorization();
            app.Run();
        }
    }
}
