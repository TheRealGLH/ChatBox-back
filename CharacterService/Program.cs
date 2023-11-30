using CharacterService.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer; // NOTE: line is newly added
using Microsoft.IdentityModel.Tokens; // NOTE: line is newly added

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateAudience = true,
		ValidAudience = "chatbox-b88f3", // NOTE: USE THE REAL DOMAIN NAME
		ValidateIssuer = true,
		ValidIssuer = "https://securetoken.google.com/chatbox-b88f3", // NOTE: USE THE REAL DOMAIN NAME
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		//IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("396B5DD9-CC75-411C-9311-5B6E1F391B89")) // NOTE: THIS SHOULD BE A SECRET KEY NOT TO BE SHARED; REPLACE THIS GUID WITH A UNIQUE ONE
	};
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EditPolicy", policy =>
        policy.Requirements.Add(new SameAuthorRequirement()));
});

builder.Services.Configure<CharacterDatabaseSettings>(
    builder.Configuration.GetSection("CharacterAPI"));

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
