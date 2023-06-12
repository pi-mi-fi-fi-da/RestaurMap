using MongoDB.Driver;
using RestaurMap.Models;

namespace RestaurMap.Services;

public class RestaurantsService : IRestaurantsService
{
    private readonly IMongoCollection<Restaurant> _RestaurantsCollection;

    public RestaurantsService(IMongoCollection<Restaurant> RestaurantsCollection)
    {
        _RestaurantsCollection = RestaurantsCollection;
    }

    public async Task<List<Restaurant>> GetAllAsync(CancellationToken cancellationToken) =>
        await _RestaurantsCollection.Find(_ => true).ToListAsync(cancellationToken);

    public async Task<Restaurant?> GetOneAsync(string id, CancellationToken cancellationToken) =>
        await _RestaurantsCollection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task CreateAsync(Restaurant newRestaurant) =>
        await _RestaurantsCollection.InsertOneAsync(newRestaurant);

    public async Task RemoveAsync(string id, CancellationToken cancellationToken) =>
        await _RestaurantsCollection.DeleteOneAsync(x => x.Id == id, cancellationToken);
}
