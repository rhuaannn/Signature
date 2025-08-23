
using Signature.Exception;

namespace Signature.Exception.Exception
{
    public class ErroOnValidationException : ExceptionBase
    {
        public List<string> Errors { get; set; }

        public ErroOnValidationException(List<string> errors)
        {
            Errors = errors;
        }

    }
}
