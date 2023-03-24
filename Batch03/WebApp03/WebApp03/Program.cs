using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Sagger
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
        Title = "Weather API",
        Version = "v1",
        Description = "This API for sample only"
    });
});

// Add JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"])),
            ValidIssuers = builder.Configuration.GetSection("Jwt:Issuers").Get<string[]>(),
            ValidAudiences = builder.Configuration.GetSection("Jwt:Audiences").Get<string[]>(),
        };
    
    });

var app = builder.Build();

// Use Swagger
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API");
});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Add Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
