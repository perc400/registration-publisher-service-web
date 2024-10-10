using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);
string connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
	?? builder.Configuration.GetConnectionString("PostgreSqlProvider");
builder.Services.AddDbContext<WebApiContext>(options =>
            options.UseNpgsql(connectionString)
        );
builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();
builder.Services.AddControllers();

System.Console.WriteLine($"Connection string: {connectionString}");

var app = builder.Build();
app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WebApiContext>();
    db.Database.Migrate();
}

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
}
);
#pragma warning restore ASP0014

app.Run();
