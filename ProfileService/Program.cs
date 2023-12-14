using ChatBoxSharedObjects.Messages;
using ChatBoxSharedObjects.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProfileService.Messages;
using ProfileService.Views;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddSingleton<IProfileStore, ProfileStoreDatabase>();
builder.Services.AddSingleton<IRabbitMqConsumerService, ProfileConsumerService>();
builder.Services.AddHostedService<ConsumerHostedService>();
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

builder.Services.Configure<MongoDatabaseSettings>(
	builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<RabbitMqSettings>(
	builder.Configuration.GetSection("RabbitMqConfiguration"));

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
