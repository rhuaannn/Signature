using Signature.Exception.Exception;

namespace Signature.Domain.ValueObjects
{
    public class Description
    {
        public string Value { get; private set; }

        public Description(string value)
        {
            if (!IsValid(value))
            {
                // Usar DomainValidationException em vez de ArgumentException
                throw new DomainValidationException("Description must be non-empty and up to 50 characters long.");
            }
            Value = value;
        }

        public bool IsValid(string value) => !string.IsNullOrWhiteSpace(value) && value.Length <= 50;

        public static implicit operator string(Description description) => description.Value;

        public override string ToString() => Value;
    }
}