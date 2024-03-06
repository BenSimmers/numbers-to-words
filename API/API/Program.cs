namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
        
        // hard code the PORT to 5000
        builder.WebHost.UseUrls("http://localhost:5000/");
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: myAllowSpecificOrigins,
                policy  =>
                {
                    policy.WithOrigins("http://localhost:5000",
                            "http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseCors(myAllowSpecificOrigins);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();
        Console.WriteLine("Swagger is running on http://localhost:5000/swagger/index.html");

        app.Run();

    }
}