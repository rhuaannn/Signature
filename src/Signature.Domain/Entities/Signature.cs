using Signature.Domain.EntiteBase; // Note o 'EntiteBase' no using
using Signature.Domain.Enum;
using Signature.Domain.ValueObjects;
using Signature.Exception.Exception;
using System;
using System.Collections.Generic;

namespace Signature.Domain.Entities
{
    public class Signature : EntityBase
    {
        private string description;
        private DateTime createdDate;

        public string Name { get; private set; } = string.Empty;
        public Description Description { get; private set; }
        public DateTime StartDate { get; private set; } = DateTime.Now;
        public DateTime? EndDate { get; private set; }
        public SignatureEnum Situation { get; set; }
        public ICollection<StudentSignature> StudentSignatures { get; private set; } = new List<StudentSignature>();

        protected Signature()
        {

        }

        public Signature(string name, Description description, DateTime startDate, DateTime? endDate, int situation)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainValidationException("O nome não pode ser nulo ou vazio.");
            if (description == null)
                throw new DomainValidationException("A descrição não pode ser nula.");
            if (!System.Enum.IsDefined(typeof(SignatureEnum), situation))
                throw new DomainValidationException($"Valor inválido para situação: {situation}");

            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Situation = (SignatureEnum)situation;
        }


        public void AddStudent(StudentSignature studentSignature)
        {
            if (studentSignature == null)
            {
                throw new ArgumentNullException(nameof(studentSignature));
            }
            if (SignatureEnum.Cancelled == Situation)
            {
                throw new InvalidOperationException("Cannot add a student to an inactive signature.");
            }
            StudentSignatures.Add(studentSignature);
        }
    }
}