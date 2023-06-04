namespace RestaurMap.Repository;

public interface IEntity<T>
{
    public T Id { get; set; }
}
