using LojaApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton<ProdutoRepository>(provider => new ProdutoRepository(connection));
builder.Services.AddSingleton<UsuarioRepository>(provider => new UsuarioRepository(connection));
builder.Services.AddSingleton<CarrinhoRepository>(provider => new CarrinhoRepository(connection));
builder.Services.AddSingleton<PedidoRepository>(provider => new PedidoRepository(connection));


//Realiza a leitura da conexão com o banco
builder.Services.AddSingleton<ProdutoRepository>(
    provider =>
    new ProdutoRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

//Swagger Parte 1
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

//Swagger Parte 2
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud Loja V1");
        c.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
