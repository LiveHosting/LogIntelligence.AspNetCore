using LogIntelligence.AspNetCore;
using LogIntelligence.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLogIntelligence(options =>
{
    options.ApiKey = Guid.Parse("db7c007f-a323-4d78-9342-4322b7403bbe");
    options.LogID = Guid.Parse("db7c007f-a323-4d78-9342-4322b7403bbe");
    options.ApplicationName = "AspNetCore 8.0 Example WebApp using LogIntelligence.Extensions.Logging";
});

// The LogIntelligence Logger Provider can log any log level, but we recommend only to log warning and up
builder.Logging.AddFilter<LogIntelligenceLoggerProvider>(null, LogLevel.Information);

// Add a filter to exclude information logs from System.Net.Http.HttpClient.LogIntelligenceClient.LogicalHandler
//builder.Logging.AddFilter<LogIntelligenceLoggerProvider>("System.Net.Http.HttpClient.LogIntelligenceClient.LogicalHandler", Microsoft.Extensions.Logging.LogLevel.Warning);
//builder.Logging.AddFilter<LogIntelligenceLoggerProvider>("System.Net.Http.HttpClient.LogIntelligenceClient.ClientHandler", Microsoft.Extensions.Logging.LogLevel.Warning);

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
