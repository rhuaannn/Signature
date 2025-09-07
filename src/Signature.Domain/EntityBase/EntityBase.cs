namespace Signature.Domain.EntiteBase
{
    public class EntityBase
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        protected EntityBase()
        {

        }
        public EntityBase(Guid id)
        {
            Id = id;
            Id = Guid.NewGuid();

        }
    }
}
