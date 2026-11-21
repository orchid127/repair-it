using Microsoft.EntityFrameworkCore;
using RepairIT.Shared;
using RepairIT.UsersService;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbPath = Path.GetFullPath(
    Path.Combine(
        builder.Environment.ContentRootPath,
        "../RepairIT.Shared/repairitinfo.db"
    )
);

builder.Services.AddDbContext<RepairITDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")
);

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
