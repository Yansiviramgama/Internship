using Yansi_KalmTraining.Model.Config;
using Yansi_KalmTraining.Model.Main;
using Yansi_KalmTraining.Services.JWTAuthentication;
using Yansi_KalmTraining;
using Yansi_KalmTraining.Logger;
using Yansi_KalmTraining.MiddelWare;
using Yansi_KalmTraining.Model.SMTPSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<DataConfig>(builder.Configuration.GetSection("ConnectionStrings"));
RegisterServices.RegisterService(builder.Services);
builder.Services.Configure<SMTPSettings>(builder.Configuration.GetSection("SMTPSettings"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<IJWTAuthenticationServices, JWTAuthenticationServices>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseMiddleware<CustomMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
