using Signature.Application.ViewModels;
using Signature.Domain.Enum;
using Signature.Exception.Exception;

namespace Signature.Application.Mapping
{
    public static class MappingSignature
    {
        public static Domain.Entities.Signature ToDomain(this CreateViewModelSignature viewModel)
        {
            return new Domain.Entities.Signature(
                viewModel.Name,
                new Domain.ValueObjects.Description(viewModel.Description),
                viewModel.CreatedDate,
                null,
                viewModel.Situation ?? 0
            );
        }

        public static CreateViewModelSignature ToViewModel(this Domain.Entities.Signature signature)
        {
            return new CreateViewModelSignature(
                signature.Name,
                (int)signature.Situation,
                signature.Description.Value,
                signature.StartDate
            );
        }

        private static SignatureEnum ToSignatureEnum(int value)
        {
            if (!System.Enum.IsDefined(typeof(SignatureEnum), value))
                throw new DomainValidationException($"Valor inválido para SignatureEnum: {value}");

            return (SignatureEnum)value;
        }

        private static int ToInt(SignatureEnum signatureEnum)
        {
            return (int)signatureEnum;
        }
    }
}