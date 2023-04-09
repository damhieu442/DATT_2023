using DATT.k14_2023.DL;
using DATT.K14_2023.BL.BaseBL;
using DATT.k14_2023.DL.BaseDL;
using DATT.K14_2023.BL.CustomerBL;
using DATT.k14_2023.DL.CustomerDL;
using DATT.K14_2023.BL.ShoeBL;
using DATT.k14_2023.DL.ShoeDL;
using DATT.K14_2023.BL.CategoryBL;
using DATT.k14_2023.DL.CategoryDL;
using DATT.k14_2023.COMMON.Constants;
using DATT.K14_2023.BL.EvaluateBL;
using DATT.k14_2023.DL.EvaluateDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = false);
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddTransient(typeof(IBaseDL<>), typeof(BaseDL<>));

builder.Services.AddTransient<ICustomerBL, CustomerBL>();
builder.Services.AddTransient<ICustomerDL, CustomerDL>();

builder.Services.AddTransient<IShoeBL, ShoeBL>();
builder.Services.AddTransient<IShoeDL, ShoeDL>();

builder.Services.AddTransient<ICategoryBL, CategoryBL>();
builder.Services.AddTransient<ICategoryDL, CategoryDL>();

builder.Services.AddTransient<IEvaluateBL, EvaluateBL>();
builder.Services.AddTransient<IEvaluateDL, EvaluateDL>();

DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddHttpContextAccessor();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
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

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
