namespace RSharp.API.Entity
{
    public abstract class Entity
    {
        public IPosition Position { get; }

        public Entity(IPosition position)
        {
            Position = position;
        }
    }
}