var builder = WebApplication.CreateBuilder(args);

// Ajout des contrôleurs
builder.Services.AddControllers();

// Configuration de la base de données
var connectionString = builder.Configuration.GetConnectionString("GABCnxStr");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("La chaîne de connexion 'GABCnxStr' est introuvable dans appsettings.json.");
}

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

// Injection de dépendances (Managers)
builder.Services.AddScoped<IClientManager, ClientManager>();
builder.Services.AddScoped<ICompteBancaireManager, CompteBancaireManager>();
builder.Services.AddScoped<ICarteBancaireManager, CarteBancaireManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<ITransactionManager, TransactionManager>();

// Configuration des paramètres JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Configuration de Swagger (Documentation API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "systemeGABWebApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Ajouter le token ainsi : \"Bearer xxxx\" où xxxx est votre token d'authentification",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

// Configuration de l'authentification JWT

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Secret))
{
    throw new Exception("La configuration JWT est invalide. Vérifiez 'JwtSettings' dans appsettings.json.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configuration du pipeline HTTP

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
