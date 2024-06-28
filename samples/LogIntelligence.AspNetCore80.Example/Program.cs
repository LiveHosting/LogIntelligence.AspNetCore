using LogIntelligence.AspNetCore.Extensions;
using LogIntelligence.Client;

var builder = WebApplication.CreateBuilder(args);

// IMPORTANT: this is where the magic happens. Insert your api key found on the profile as well as the log id of the log to log to.
builder.Services.AddLogIntelligence(options =>
{
    options.ApiKey = Guid.Parse("db7c007f-a323-4d78-9342-4322b7403bbe");
    options.LogID = new Guid("db7c007f-a323-4d78-9342-4322b7403bbe");

    // Optional application name
    options.Application = "ASP.NET Core Razor Pages WebApp 8.0 Example Application";

    // Add event handlers etc. like this:
    //options.OnMessage = msg =>
    //{
    //    msg.Version = "8.0.0";
    //};
});

// ApiKey and LogId can be configured in appsettings.json as well, by calling the Configure-method instead of AddElmahIo.
//builder.Services.Configure<LogIntelligenceApiClientOptions>(builder.Configuration.GetSection("LogIntelligenceApiClientOptions"));
// Still need to call this to register all dependencies
//builder.Services.AddLogIntelligence();

// If you configure ApiKey and LogId through appsettings.json, you can still add event handlers, configure handled status codes, etc.
//builder.Services.Configure<LogIntelligenceApiClientOptions>(o =>
//{
//    o.OnMessage = msg =>
//    {
//        msg.Version = "8.0.0";
//    };
//});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
