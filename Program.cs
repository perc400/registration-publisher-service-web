using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApiContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlProvider"))
        );
builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
}
);
#pragma warning restore ASP0014

app.Run();
