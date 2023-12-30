using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Invio.Extensions.Authentication.JwtBearer;
using ChatBoxSharedObjects.Connectors;
using ChatBoxSharedObjects.Settings;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ICharacterDatabaseConnector, CharacterDatabaseConnectorMongo>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.Authority = "https://securetoken.google.com/chatbox-b88f3";
	options.AddQueryStringAuthentication();
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateAudience = true,
		ValidAudience = "chatbox-b88f3",
		ValidateIssuer = true,
		ValidIssuer = "https://securetoken.google.com/chatbox-b88f3",
		ValidateLifetime = true,
	};
});

builder.Services.Configure<MongoDatabaseSettings>(
	builder.Configuration.GetSection("MongoDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWebSockets();

app.MapControllers();

app.Run();
