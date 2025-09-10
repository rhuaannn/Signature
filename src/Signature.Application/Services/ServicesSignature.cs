using Signature.Application.Interface;
using Signature.Domain.Entities;
using Signature.Exception.Exception;
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
            Validate(signature);

            //if (createSignature.Situation != Domain.Enum.SignatureEnum.Active)
            //{
            //    throw new ArgumentException("A assinatura deve estar ativa ao ser criada.", nameof(signature.Situation));
            //}
            var createSignature = await _signatureRepository.CreateSignatureAsync(signature);
            return createSignature;
        }

        private void Validate(Domain.Entities.Signature signature)
        {
            var validator = new SignatureValidation();
            var result = validator.Validate(signature);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErroOnValidationException(errors);
            }
        }
    }
}
