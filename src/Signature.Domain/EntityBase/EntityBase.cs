namespace Signature.Domain.EntiteBase
{
    public class EntityBase
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}
