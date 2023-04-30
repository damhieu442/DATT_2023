using DATT.k14_2023.DL;
using DATT.K14_2023.BL.IBaseBL;
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
using DATT.k14_2023.DL.CartDL;
using DATT.K14_2023.BL.SizeBL;
using DATT.k14_2023.DL.SizeDL;
using DATT.K14_2023.BL.FeedBackBL;
using DATT.k14_2023.DL.FeedBackDL;
using DATT.K14_2023.BL.CartDetailBL;
using DATT.k14_2023.DL.CartDetailDL;
using DATT.K14_2023.BL.BillDetailBL;
using DATT.k14_2023.DL.BillDetailDL;
using DATT.K14_2023.BL.BillBL;
using DATT.k14_2023.DL.BillDL;
using EmailService;
using Stripe;
using DATT.K14_2023.BL.DashBoardBL;
using DATT.k14_2023.DL.DashBoardDL;

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

builder.Services.AddTransient<ICartDL, CartDL>();

builder.Services.AddTransient<ISizeBL, SizeBL>();
builder.Services.AddTransient<ISizeDL, SizeDL>();

builder.Services.AddTransient<IFeedBackBL, FeedBackBL>();
builder.Services.AddTransient<IFeedBackDL, FeedBackDL>();

builder.Services.AddTransient<ICartDetailBL, CartDetailBL>();
builder.Services.AddTransient<ICartDetailDL, CartDetailDL>();

builder.Services.AddTransient<IBillDetailBL, BillDetailBL>();
builder.Services.AddTransient<IBillDetailDL, BillDetailDL>();

builder.Services.AddTransient<IBillBL, BillBL>();
builder.Services.AddTransient<IBillDL, BillDL>();

builder.Services.AddTransient<IDashBoardBL, DashBoardBL>();
builder.Services.AddTransient<IDashBoardDL, DashBoardDL>();

DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");
var email = builder.Configuration.GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(email);
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// Add Cors
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseRouting();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
