using AspNetCoreRateLimit;
using CarLocadora.API.Extensoes;

var builder = WebApplication.CreateBuilder(args);

//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connection));

builder.Services.ConfigurarServicos(builder.Configuration);
builder.Services.ConfigurarJWT();
builder.Services.ConfigurarSwagger();

builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();


// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseIpRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();