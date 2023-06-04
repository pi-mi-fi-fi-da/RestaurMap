using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using RestaurMap.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
// Convention
var pack = new ConventionPack
{
    new CamelCaseElementNameConvention(),
    new StringIdStoredAsObjectIdConvention()
};
ConventionRegistry.Register("kradneo_convenction", pack, _ => true);

//MongoDB
MongoUrl url = new MongoUrl(builder.Configuration.GetConnectionString("MongoDB"));
MongoClientSettings settings = MongoClientSettings.FromUrl(url);
MongoClient client = new MongoClient(settings);
IMongoDatabase database = client.GetDatabase(url.DatabaseName);

//Colections
IMongoCollection<Phrase> phrases = database.GetCollection<Phrase>("phrases");
//IMongoCollection<PhraseProduct> products = database.GetCollection<PhraseProduct>("products");

builder.Services.AddSingleton(phrases);
//builder.Services.AddSingleton(products);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

//app.Lifetime.ApplicationStarted.Register(async () =>
//{
//    //var phrases = app.Services.GetRequiredService<IMongoCollection<Phrase>>();
//    //await phrases.InsertOneAsync(new Phrase { Name = $"fraza_{DateTime.UtcNow}" });

//    //var products = app.Services.GetRequiredService<IMongoCollection<PhraseProduct>>();
//    //await products.InsertOneAsync(new PhraseProduct { PhraseName = $"fraza_{DateTime.UtcNow}" });
//});
//Scrapper scrapper = new Scrapper(new PhraseProductsService(products), new PhrasesService(phrases));
//await scrapper.TrackData(CancellationToken.None);

app.Run();
