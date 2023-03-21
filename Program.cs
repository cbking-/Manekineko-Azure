using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AzureManekineko.Data;
using Microsoft.Azure.Cosmos;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(builder => {
    builder.AddAzureAppConfiguration(options => options.Connect(new Uri("https://manekineko.azconfig.io"), new DefaultAzureCredential()));
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<CosmosClient>(serviceProvider =>
{
    return new CosmosClient(accountEndpoint: builder.Configuration["cosmoEndpoint"], new DefaultAzureCredential());
});


builder.Services.AddSingleton<FortuneDollService>();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
