using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using RestaurMap.Models;
using RestaurMap.Services;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

// Convention
var pack = new ConventionPack
{
    new CamelCaseElementNameConvention(),
    new StringIdStoredAsObjectIdConvention()
};
ConventionRegistry.Register("restaurmap_convenction", pack, _ => true);

//MongoDB
MongoUrl url = new MongoUrl(builder.Configuration.GetConnectionString("MongoDB"));
MongoClientSettings settings = MongoClientSettings.FromUrl(url);
MongoClient client = new MongoClient(settings);
IMongoDatabase database = client.GetDatabase(url.DatabaseName);

//Colections
IMongoCollection<Restaurant> restaurants = database.GetCollection<Restaurant>("restaurants");

builder.Services.AddSingleton(restaurants);

//Services
builder.Services.AddScoped<IRestaurantsService, RestaurantsService>();
builder.Services.AddScoped<Scrapper>();


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
    (
        mongoDbSettings.ConnectionString, mongoDbSettings.Name
    );



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var scraper = scope.ServiceProvider.GetService<Scrapper>();
    scraper.Scrapp();
}

app.Run();
