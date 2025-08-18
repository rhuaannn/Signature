namespace Signature.Domain.ValueObjects
{
    public class CPF
    {
        public string Value { get; private set; }
        public CPF(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new ArgumentException("Invalid CPF format.");
            }
        }
        public bool IsValid(string value)
        {
            var digitsOnly = new string(value.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length != 11)
                return false;

            if (new string(digitsOnly.ToArray().Distinct().ToArray()).Length == 1)
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(digitsOnly[i].ToString()) * (10 - i);
            int remainder = sum % 11;
            int firstVerifier = (remainder < 2) ? 0 : 11 - remainder;

            if (int.Parse(digitsOnly[9].ToString()) != firstVerifier)
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(digitsOnly[i].ToString()) * (11 - i);
            remainder = sum % 11;
            int secondVerifier = (remainder < 2) ? 0 : 11 - remainder;

            if (int.Parse(digitsOnly[10].ToString()) != secondVerifier)
                return false;

            return true;
        }
        public static implicit operator string(CPF cpf) => cpf.Value;
        public override string ToString() => Value;
    }
}
