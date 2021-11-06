using Library.Infrastructure;
using Library.WebApi.Authentication;
using Library.WebApi.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ICustomTokenManager, CustomTokenManager>();
//builder.Services.AddSingleton<ICustomTokenManager, JwtTokenManager>();

builder.Services.AddSingleton<ICustomUserManager, CustomUserManager>();

builder.Services.AddDbContext<LibraryContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseInMemoryDatabase("Library");
});

builder.Services.AddControllers();

// for identity server 4, alternative to Custom Authorization Filter inn controllers
//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.Authority = "https://localhost:5001";
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false
//        };
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("WebApiScope", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//        policy.RequireClaim("scope", "webapi");
//    });

//    options.AddPolicy("WebApiWriteScope", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//        policy.RequireClaim("scope", "write");
//    });
//});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7231")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    using (var scope = app.Services.CreateScope())
    {
        app.UseSwaggerUI(options =>
        {
            foreach (var description in
                scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        scope.ServiceProvider.GetRequiredService<LibraryContext>()
            .Database.EnsureDeleted();
        scope.ServiceProvider.GetRequiredService<LibraryContext>()
            .Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseCors();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
