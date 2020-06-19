namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}