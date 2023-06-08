using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using RestaurMap.Models.Db;
using RestaurMap.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();

//////app.Lifetime.ApplicationStarted.Register(async () =>
//////{
//////    //var phrases = app.Services.GetRequiredService<IMongoCollection<Phrase>>();
//////    //await phrases.InsertOneAsync(new Phrase { Name = $"fraza_{DateTime.UtcNow}" });

//////    //var products = app.Services.GetRequiredService<IMongoCollection<PhraseProduct>>();
//////    //await products.InsertOneAsync(new PhraseProduct { PhraseName = $"fraza_{DateTime.UtcNow}" });
//////});
//Scrapper scrapper = new Scrapper(new PhraseProductsService(products), new PhrasesService(phrases));
//await scrapper.TrackData(CancellationToken.None);

app.Run();
