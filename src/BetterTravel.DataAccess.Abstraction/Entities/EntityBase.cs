namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}