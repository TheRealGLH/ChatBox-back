using CharacterService.Messaging;
using CharacterService.Views;
using ChatBoxSharedObjects.Messages;
using ChatBoxSharedObjects.Security;
using ChatBoxSharedObjects.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICharacterStore, CharacterStoreDatabase>();
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddSingleton <IRabbitMqProducer, RabbitMqProducer> ();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.Authority = "https://securetoken.google.com/chatbox-b88f3";
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateAudience = true,
		ValidAudience = "chatbox-b88f3",
		ValidateIssuer = true,
		ValidIssuer = "https://securetoken.google.com/chatbox-b88f3",
		ValidateLifetime = true,
	};
});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("EditPolicy", policy =>
		policy.Requirements.Add(new SameAuthorRequirement()));
	options.AddPolicy("RolePolicy", policy =>
		policy.Requirements.Add(new HasCorrectRoleRequirement()));
});

builder.Services.Configure<MongoDatabaseSettings>(
	builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<RabbitMqSettings>(
	builder.Configuration.GetSection("RabbitMqConfiguration"));
builder.Services.AddSingleton<IAuthorizationHandler, AccountAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, DocumentAuthorizationHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
