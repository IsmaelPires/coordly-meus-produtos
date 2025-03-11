using Application.Services;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext no contêiner de DI
builder.Services.AddDbContext<MeusProdutosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MeuProdutos")));

// Serviços da aplicação
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
