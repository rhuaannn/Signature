using FluentValidation;
using Signature.Exception;

namespace Signature.Application.Services
{
    public class SignatureValidation : AbstractValidator<Domain.Entities.Signature>
    {
        public SignatureValidation()
        {
            RuleFor(signature => signature.Name)
                .NotEmpty().WithMessage("Name cannot be null or empty.")
                .MaximumLength(200).WithMessage(ErrorMessageException.NAME_EMPTY);
            RuleFor(signature => signature.Situation)
                .IsInEnum().WithMessage(ErrorMessageException.DESCRIPTION_EMPTY);
            RuleFor(Signature => Signature.Description)
                .NotNull().WithMessage(ErrorMessageException.SITUATION_EMPTY);
        }
    }
}
