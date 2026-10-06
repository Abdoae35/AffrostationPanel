
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

builder.Services.AddAuthorization();

// Add Connection string
builder.Services.AddDbContext<AgriContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});

// Swagger date format setup (keep this)
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new Microsoft.OpenApi.Any.OpenApiString("0000-00-00")
    });
});

// Register Repos
builder.Services.AddScoped<IAfforestationAgricultureAchievementRepo, AfforestationAgricultureAchievementRepo>();
builder.Services.AddScoped<ILocationNameRepo, LocationNameRepo>();
builder.Services.AddScoped<ITreeNameRepo, TreeNameRepo>();
builder.Services.AddScoped<ITreeTypeRepo, TreeTypeRepo>();
builder.Services.AddScoped<ITypeOfLocationRepo, TypeOfLocationRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

// Register Managers
builder.Services.AddScoped<IAfforestationAgricultureAchievementManager, AfforestationAgricultureAchievementManager>();
builder.Services.AddScoped<ITreeNameManager, TreeNameManager>();
builder.Services.AddScoped<ILocationManager, LocationManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<ITreeTypeManager, TreeTypeManager>();
builder.Services.AddScoped<ILocationTypeManager, LocationTypeManager>();

var app = builder.Build();

// Middleware order is IMPORTANT
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Authentication First then Authorization
app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();
app.Run();
