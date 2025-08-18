using Signature.Domain.EntiteBase;
using Signature.Domain.ValueObjects;

namespace Signature.Domain.Entities
{
    public class Signature : EntityBase
    {
        public string Name { get; private set; } = string.Empty;
        public Description Description { get; private set; }
        public DateTime start_date { get; private set; }
        public DateTime end_date { get; private set; }

        public ICollection<StudentSignature> StudentSignatures { get; set; }

    }
}
