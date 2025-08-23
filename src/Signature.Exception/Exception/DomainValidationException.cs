using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Exception.Exception
{
    public class DomainValidationException : ExceptionBase
    {
        public List<string> Errors { get; set; }
        public DomainValidationException(string error)
        {
            Errors = new List<string> { error };
        }
    }
}
