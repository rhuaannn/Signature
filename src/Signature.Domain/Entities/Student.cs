using Signature.Domain.EntiteBase;
using Signature.Domain.ValueObjects;

namespace Signature.Domain.Entities
{
    public class Student : EntityBase
    {
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public CPF CPF { get; private set; }
        public DateTime DateRegistration { get; private set; } = DateTime.Now;
        public ICollection<StudentSignature> StudentSignatures { get; set; }


    }
}
