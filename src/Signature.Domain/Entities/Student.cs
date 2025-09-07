using Signature.Domain.EntiteBase;
using Signature.Domain.ValueObjects;

namespace Signature.Domain.Entities
{
    public class Student : EntityBase
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public CPF CPF { get; private set; }
        public DateTime DateRegistration { get; private set; } = DateTime.Now;

        public ICollection<StudentSignature> StudentSignatures { get; private set; } = new List<StudentSignature>();

        private Student() { }

        public Student(string name, string email, string cpf)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            Name = name;
            Email = new Email(email);
            CPF = new CPF(cpf);
            DateRegistration = DateTime.Now;
            StudentSignatures = new List<StudentSignature>();
        }


        public void Update(string name, string email, string cpf)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            Name = name;
            Email = new Email(email);
            CPF = new CPF(cpf);
        }

        public void AddSignature(Signature signature, DateTime startDate)
        {
            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            if (signature.EndDate.HasValue && signature.EndDate.Value < startDate)
                throw new ArgumentException("Signature end date cannot be before the start date.", nameof(signature));

            var studentSignature = new StudentSignature(signature.Id, this.Id);

            StudentSignatures.Add(studentSignature);
        }
        public void RemoveSignature(StudentSignature studentSignature)
        {
            if (studentSignature == null)
                throw new ArgumentNullException(nameof(studentSignature));

            if (!StudentSignatures.Contains(studentSignature))
                throw new InvalidOperationException("The student signature does not exist in the student's signature list.");

            StudentSignatures.Remove(studentSignature);
        }


    }
}