using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RestaurMap.Repository;

public class Repository<TModel> : IRepository<TModel> where TModel : class, IEntity<int>
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TModel> _set;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _set = context.Set<TModel>();
    }

    public virtual async Task<TModel> Create(TModel model)
    {
        await _set.AddAsync(model);
        await Save();
        return model;
    }

    public virtual async Task Delete(TModel model)
    {

        _set.Remove(model);
        await Save();
    }

    public virtual async Task<IQueryable<TModel>> FindBy(Expression<Func<TModel, bool>> predicate)
    {
        IQueryable<TModel> result = _set.Where(predicate);
        return result;
    }

    public virtual async Task<IQueryable<TModel>> GetAll()
    {
        return _set;
    }
    public virtual async Task<TModel> GetOne(int id)
    {
        return await _set.FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task<TModel> Update(TModel model)
    {
        try
        {
            _context.Entry(model).State = EntityState.Modified;
            await Save();
        }
        catch (Exception)
        {
            throw;
        }
        return model;
    }
    public virtual async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
