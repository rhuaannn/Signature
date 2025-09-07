using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Infra.Interface
{
    public interface ISignatureRepository
    {
        public Task<Domain.Entities.Signature> CreateSignatureAsync(Domain.Entities.Signature signature);
    }
}
