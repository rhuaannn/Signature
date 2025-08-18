using System;
using Signature.Domain.ValueObjects;

namespace Signature.Domain.Entities
{
    public class StudentSignature
    {
        public Guid FKIdSignature { get; private set; }
        public Guid FKIdStudent { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public Signature Signature { get; set; }
        public Student Student { get; set; }

        protected StudentSignature() { }

        public StudentSignature(Guid signatureId, Guid studentId)
        {
            if (signatureId == Guid.Empty) throw new ArgumentException("Signature ID cannot be empty.", nameof(signatureId));
            if (studentId == Guid.Empty) throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            FKIdSignature = signatureId;
            FKIdStudent = studentId;
            StartDate = DateTime.Now;
        }

        public void SetEndDate(DateTime endDate)
        {
            if (endDate <= StartDate)
            {
                throw new ArgumentException("End date must be after the start date.");
            }
            EndDate = endDate;
        }
    }
}