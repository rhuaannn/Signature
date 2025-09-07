using Signature.Application.ViewModels;

namespace Signature.Application.Mapping
{
    public static class MappingSignature
    {
        public static Domain.Entities.Signature ToDomain(this CreateViewModelSignature viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return new Domain.Entities.Signature(
                viewModel.Name,
                new Domain.ValueObjects.Description(viewModel.Description),
                viewModel.CreatedDate,
                null,
                viewModel.Situation
            );
        }
        public static CreateViewModelSignature ToViewModel(this Domain.Entities.Signature signature)
        {
            if (signature == null)
            {
                throw new ArgumentNullException(nameof(signature));
            }


            return new CreateViewModelSignature(
                signature.Name,
                (int)signature.Situation,
                signature.Description.Value,
                signature.StartDate
            );
        }
    }
}

