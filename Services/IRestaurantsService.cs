﻿using RestaurMap.Models.Db;

namespace RestaurMap.Services;

public interface IRestaurantsService
{
    public Task<List<Restaurant>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Restaurant?> GetOneAsync(string id, CancellationToken cancellationToken);
    public Task CreateAsync(Restaurant newRestaurant, CancellationToken cancellationToken);
    public Task RemoveAsync(string id, CancellationToken cancellationToken);
}