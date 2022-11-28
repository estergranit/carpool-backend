using Carpool.Repositories.GeneratedModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var CarpoolAppOrigin = "CarpoolAppOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CarpoolAppOrigin,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000", "https://carpoolservice.onrender.com")
                          .AllowAnyHeader().AllowAnyMethod(); ;
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration["CarpoolDBConnectionString"];
builder.Services.AddDbContext<CarpoolDBContext>(options => options.UseNpgsql(cs));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CarpoolAppOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();
