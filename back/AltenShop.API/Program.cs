using AltenShop.Application;
using AltenShop.Application.Ports.Services;
using AltenShop.Domain.ValueObjects;
using AltenShop.Infrastructure;
using AltenShop.Infrastructure.Persistence;
using AltenShop.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------------------------
// Add Core & Infrastructure Services
// -----------------------------------------------------------------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger & JWT integration
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "AltenShop API",
		Version = "v1",
		Description = "E-commerce backend for Alten technical test"
	});

	// Enable JWT authentication in Swagger UI
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid JWT token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

//Register application 
builder.Services.AddApplication();

// Register Infrastructure dependencies (includes DbContext)
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminEmailOnly", policy =>
		policy.RequireAssertion(context =>
		{
			var email = context.User.FindFirstValue(ClaimTypes.Email);
			return email != null && email.Equals("admin@admin.com", StringComparison.OrdinalIgnoreCase);
		}));
});


// JWT token service (Application → Infrastructure)
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Authentication
builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new()
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
			)
		};
	});

// API Versioning
builder.Services.AddApiVersioning(opt =>
{
	opt.DefaultApiVersion = new ApiVersion(1, 0);
	opt.AssumeDefaultVersionWhenUnspecified = true;
	opt.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(opt =>
{
	opt.GroupNameFormat = "'v'VVV";
	opt.SubstituteApiVersionInUrl = true;
});

// Health Checks
builder.Services.AddHealthChecks()
	.AddNpgSql(
		builder.Configuration.GetConnectionString("DefaultConnection")!,
		name: "postgresql",
		tags: new[] { "db", "ready" }
	);

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
	options.AddFixedWindowLimiter("fixed", opt =>
	{
		opt.PermitLimit = 100; // 100 requests per minute
		opt.Window = TimeSpan.FromMinutes(1);
		opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		opt.QueueLimit = 2;
	});
});

// -----------------------------------------------------------------------------
// Build Application
// -----------------------------------------------------------------------------
var app = builder.Build();

// Apply EF Core migrations and seed data
await app.Services.InitializeDatabaseAsync();

// -----------------------------------------------------------------------------
// Middleware Pipeline
// -----------------------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "AltenShop API v1");
		c.RoutePrefix = string.Empty;
	});
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
