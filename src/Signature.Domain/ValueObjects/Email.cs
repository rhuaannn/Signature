using Signature.Domain.Entities;
using Signature.Exception;
using Signature.Exception.Exception;
using System.Net.Mail;

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
                throw new DomainValidationException(ErrorMessageException.BADREQUEST);
            }
        }
        public bool IsValid(string value)
        {

            try
            {
                var addr = new MailAddress(value);
                return addr.Address == value;
            }
            catch
            {
                return false;
            }

        }
        public static implicit operator string(Email email) => email.Value;
        public override string ToString() => Value;
    }
}
