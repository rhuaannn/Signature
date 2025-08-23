using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Exception.ErrorJson
{
    public class ResponseErrorJson
    {
        public List<string> Errors { get; set; }
        public ResponseErrorJson(List<string> errors)
        {
            Errors = errors;
        }

        public ResponseErrorJson(string errors)
        {
            Errors = new List<string> { errors };

        }
    }
}
