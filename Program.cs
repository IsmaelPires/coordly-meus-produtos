using Application.Services;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext no cont�iner de DI
builder.Services.AddDbContext<MeusProdutosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MeuProdutos")));

// Servi�os da aplica��o
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
