namespace TodoList.Domain.Entities
{
    public abstract class Entity<TprimaryKey>
    {
        public TprimaryKey Id { get; set; }

    }

    public abstract class Entity : Entity<int>
    {

    }
}
