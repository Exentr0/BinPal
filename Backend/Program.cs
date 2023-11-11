using Azure.Storage.Blobs;
using Backend.Data;
using Backend.Models;
using Backend.Storage;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var service = new AzureBlobService(builder.Configuration);

//Test Azure Blob Upload
await service.UploadFilesAsync();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<UserTest>, UserTestValidator>();
builder.Services.AddScoped<IValidator<CartItem>, CartItemValidator>();
builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();
builder.Services.AddScoped<IValidator<Item>, ItemValidator>();
builder.Services.AddScoped<IValidator<ItemCategory>, ItemCategoryValidator>();
builder.Services.AddScoped<IValidator<ItemReview>, ItemReviewValidator>();
builder.Services.AddScoped<IValidator<PaymentInfo>, PaymentInfoValidator>();
builder.Services.AddScoped<IValidator<Purchase>, PurchaseValidator>();
builder.Services.AddScoped<IValidator<ShoppingCart>, ShoppingCartValidator>();
builder.Services.AddScoped<IValidator<Software>, SoftwareValidator>();
builder.Services.AddScoped<IValidator<SoftwareCategory>, SoftwareCategoryValidator>();
builder.Services.AddScoped<IValidator<PaymentMethod>, PaymentMethodValidator>();


//Add Fluent Validation 
builder.Services.AddFluentValidation(fv =>
{
    fv.ImplicitlyValidateChildProperties = true;
    fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options => options.AddPolicy(name: "UserTestOrigins", policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("UserTestOrigins");

app.UseHttpsRedirection();
app.UseRouting(); // Add this line to enable routing

app.UseAuthorization();
app.MapControllers();

app.Run();