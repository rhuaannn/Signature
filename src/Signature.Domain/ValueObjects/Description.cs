using Signature.Exception.Exception;

namespace Signature.Domain.ValueObjects
{
    public class Description
    {
        public string Value { get; private set; }
        public Description(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new DomainValidationException("Description must be non-empty and up to 50 characters long.");
            }
        }

        public bool IsValid(string value) => !string.IsNullOrWhiteSpace(Value) && Value.Length < 50;

        public static implicit operator string(Description description) => description.Value;
        public override string ToString() => Value;

    }
}
