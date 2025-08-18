namespace Signature.Domain.Entities
{
    public class StudentSignature
    {
        public Guid FKIdSignature { get; private set; }
        public Guid FKIdStudent { get; private set; }
        public DateTime StartDate { get; private set; } = DateTime.Now;
        public DateTime? EndDate { get; private set; }

        public Signature Signature { get; private set; }
        public Student Student { get; private set; }
    }
}
