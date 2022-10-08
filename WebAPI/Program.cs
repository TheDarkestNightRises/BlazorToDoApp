using Domain.Interfaces;
using EfcData.Context;
using EfcData.DaoImpl;
using FileData.DataAccess;

// using (ToDoContext ctx = new())
// {
//     ctx.Seed();
// }

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITodoHome, TodoSqliteDao>();
builder.Services.AddDbContext<ToDoContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();