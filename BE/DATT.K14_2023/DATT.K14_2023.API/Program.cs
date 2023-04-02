using DATT.k14_2023.DL;
using DATT.K14_2023.BL.BaseBL;
using DATT.k14_2023.DL.BaseDL;
using DATT.K14_2023.BL.CustomerBL;
using DATT.k14_2023.DL.CustomerDL;
using DATT.K14_2023.BL.ShoeBL;
using DATT.k14_2023.DL.ShoeDL;
using DATT.K14_2023.BL.CategoryBL;
using DATT.k14_2023.DL.CategoryDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = false);
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));

builder.Services.AddScoped<ICustomerBL, CustomerBL>();
builder.Services.AddScoped<ICustomerDL, CustomerDL>();

builder.Services.AddScoped<IShoeBL, ShoeBL>();
builder.Services.AddScoped<IShoeDL, ShoeDL>();

builder.Services.AddScoped<ICategoryBL, CategoryBL>();
builder.Services.AddScoped<ICategoryDL, CategoryDL>();

DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

// Add Cors
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Convert output to pascalcase
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corspolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
