using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Application.Interface
{
    public interface ISignature
    {
        Task<Domain.Entities.Signature> CreateSignatureAsync(Domain.Entities.Signature signature);
    }
}
