namespace Signature.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }
        public Email(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new ArgumentException("Invalid email format.");
            }
        }
        public bool IsValid(string value)
        {

            var addr = new System.Net.Mail.MailAddress(value);
            return addr.Address == value;


        }
        public static implicit operator string(Email email) => email.Value;
        public override string ToString() => Value;
    }
}
