using Signature.Domain.EntiteBase; // Note o 'EntiteBase' no using
using Signature.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Signature.Domain.Entities
{
    public class Signature : EntityBase
    {
        public string Name { get; private set; } = string.Empty;
        public Description Description { get; private set; }
        public DateTime StartDate { get; private set; } = DateTime.Now;
        public DateTime? EndDate { get; private set; }

        public ICollection<StudentSignature> StudentSignatures { get; private set; } = new List<StudentSignature>();

        protected Signature() { }

        public Signature(string name, Description description, DateTime startDate, DateTime? endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            if (description == null)
                throw new ArgumentNullException(nameof(description));

            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void AddStudent(StudentSignature studentSignature)
        {
            if (studentSignature == null)
            {
                throw new ArgumentNullException(nameof(studentSignature));
            }
            StudentSignatures.Add(studentSignature);
        }
    }
}