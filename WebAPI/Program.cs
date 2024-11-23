using Application.Commons;
using Infrastructure;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

//add redis configuration
var configuration = builder.Configuration.GetSection("RedisConfiguration").Get<RedisConfiguration>();
var RedisConf = new ConfigurationOptions
{
    EndPoints = { configuration.Endpoints },
    User = configuration.User,
    Password = configuration.Password,
};
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(RedisConf));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddInfrastructuresService();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
