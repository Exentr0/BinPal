using System.Text;
using Backend.Data;
using Backend.Models;
using Backend.Services;
using Backend.Services.Storage;
using Backend.Storage;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;


var builder = WebApplication.CreateBuilder(args);


//Azure Storage Services
builder.Services.AddScoped<UserPFPBlobService>();
builder.Services.AddScoped<ItemPicturesBlobService>();
builder.Services.AddScoped<ItemReleasesBlobService>();
builder.Services.AddScoped<SoftwarePicturesBlobService>();
builder.Services.AddScoped<ItemAddingService>();

// Add services to the container.
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<CartItem>, CartItemValidator>();
builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();
builder.Services.AddScoped<IValidator<Item>, ItemValidator>();
builder.Services.AddScoped<IValidator<ItemCategory>, ItemCategoryValidator>();
builder.Services.AddScoped<IValidator<ItemReview>, ItemReviewValidator>();
builder.Services.AddScoped<IValidator<PaymentDetails>, PaymentDetailsValidator>();
builder.Services.AddScoped<IValidator<Purchase>, PurchaseValidator>();
builder.Services.AddScoped<IValidator<ShoppingCart>, ShoppingCartValidator>();
builder.Services.AddScoped<IValidator<Software>, SoftwareValidator>();
builder.Services.AddScoped<IValidator<SoftwareCategory>, SoftwareCategoryValidator>();
builder.Services.AddScoped<IValidator<PaymentMethod>, PaymentMethodValidator>();
builder.Services.AddScoped<IValidator<Plugin>, PluginValidator>();
builder.Services.AddScoped<IValidator<ItemSoftware>, SoftwareItemValidator>();
builder.Services.AddScoped<IValidator<ItemPlugin>, ItemPluginValidator>();

//Add Fluent Validation 
builder.Services.AddFluentValidation(fv =>
{
    fv.ImplicitlyValidateChildProperties = true;
    fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddCookie(cfg => cfg.SlidingExpiration = true).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:5000",
        ValidateLifetime = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
    options.Authority = "http://localhost:5000";
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.Audience = "BinPal";
    options.Configuration = new OpenIdConnectConfiguration();
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(options => options.AddPolicy(name: "UserTestOrigins",
    policy => { policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader(); }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("UserTestOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();