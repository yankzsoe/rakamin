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

var app = builder.Build();

// Use Swagger
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API");
});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
