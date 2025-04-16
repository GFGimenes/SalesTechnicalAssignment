using Microsoft.EntityFrameworkCore;
using SalesTechnicalAssignment.Data;
using SalesTechnicalAssignment.Service;
using SalesTechnicalAssignment.Interface;

var builder = WebApplication.CreateBuilder(args);
        
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("DeveloperStoreDb"));

builder.Services.AddScoped<ISalesService, SaleService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    FakeSeeder.Seed(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
