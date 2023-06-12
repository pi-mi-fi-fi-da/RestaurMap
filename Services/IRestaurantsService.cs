using RestaurMap.Models;

namespace RestaurMap.Services;

public interface IRestaurantsService
{
    public Task<List<Restaurant>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Restaurant?> GetOneAsync(string id, CancellationToken cancellationToken);
    public Task CreateAsync(Restaurant newRestaurant);
    public Task RemoveAsync(string id, CancellationToken cancellationToken);
}
