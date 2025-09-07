using Signature.Application.Interface;
using Signature.Infra.ContextDB;
using Signature.Infra.Interface;
using Signature.Infra.Repositories;

namespace Signature.Application.Services
{
    public class ServicesSignature : ISignature
    {
        private readonly ISignatureRepository _signatureRepository;
        public ServicesSignature(ISignatureRepository signatureRepository)
        {
            _signatureRepository = signatureRepository;

        }

        public async Task<Domain.Entities.Signature> CreateSignatureAsync(Domain.Entities.Signature signature)
        {
            if (signature == null)
            {
                throw new ArgumentException("Signature cannot be null", nameof(signature));
            }
            var createSignature = await _signatureRepository.CreateSignatureAsync(signature);
            return createSignature;
        }
    }
}
