using WebAppIntermediate2.Services;

namespace WebAppIntermediate2 {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient<DummyRestApiService>(opt => {
                opt.BaseAddress = new Uri(builder.Configuration.GetSection("DummyRestapi").GetSection("baseUrl").Value);
                opt.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Add AddMemoryCache to active memory cache.
            builder.Services.AddMemoryCache();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}