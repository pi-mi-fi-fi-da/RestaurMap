using System.Linq.Expressions;

namespace RestaurMap.Repository;

public interface IRepository<TModel> where TModel : IEntity<int>
{
    Task<TModel> Create(TModel model);
    Task<TModel> Update(TModel model);
    Task Delete(TModel model);
    Task<TModel> GetOne(int id);
    Task<IQueryable<TModel>> GetAll();
    Task<IQueryable<TModel>> FindBy(Expression<Func<TModel, bool>> predicate);
}
