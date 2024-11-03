using Dashboard.Server.Consumers;
using Dashboard.Server.Hubs;
using Infrastructure.Extensions;
using Infrastructure.Json;
using Infrastructure.MassTransit;
using Infrastructure.Redis;
using Infrastructure.Storage;
using Infrastructure.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(o => o.SerializerSettings.Converters.AddRange(JsonUtils.Converters));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDashboardHubContext, DashboardHubContext>();
builder.Services.AddMassTransitConsumersConfig<CalculixProgressNotificationConsumer>(builder.Configuration);
builder.Services.AddSignalR(o => o.EnableDetailedErrors = true).AddNewtonsoftJsonProtocol(options => options.PayloadSerializerSettings.Converters.AddRange(JsonUtils.Converters));

builder.Services.AddStorage(builder.Configuration);
    
builder.Services.AddCors(o =>
{
    o.AddPolicy("local", p =>
    {
        p.AllowAnyOrigin();
        p.AllowAnyHeader();
        p.AllowAnyMethod();
    });
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

var app = builder.Build();

app.UseResponseCompression();
if (!app.Environment.IsDevelopment())
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();


app.UseCors("local");
app.MapControllers();
app.MapHub<DashboardHub>("/dashboardHub");
app.MapFallbackToFile("/index.html");

await Wait.Execute();
await app.RunAsync();
