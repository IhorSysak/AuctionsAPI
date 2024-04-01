using AuctionsAPI.Cache;
using AuctionsAPI.Context;
using AuctionsAPI.Controllers;
using AuctionsAPI.Interfaces;
using AuctionsAPI.Services;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Converters;
using Serilog;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddDbContext<AuctionsContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("AuctionsContext")));

var redisCacheSettings = new RedisCacheSettings();
builder.Services.AddSingleton(redisCacheSettings);

builder.Services.AddStackExchangeRedisCache(option => option.Configuration = redisCacheSettings.ConnectionString);
builder.Services.AddScoped<IAuctionsService, AuctionsService>();
builder.Services.AddSingleton<IResponceCacheService, ResponceCacheService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUriService>(provider =>
{
    var accessor = provider.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
    return new UriService(absoluteUri);
});

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        CamelCaseText = true
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
