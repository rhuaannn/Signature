using FluentValidation;
using Signature.Domain.Entities;
using Signature.Domain.ValueObjects;
using Signature.Exception;

namespace Signature.Application.Services
{
    public class StudentValidation : AbstractValidator<Student>
    {
        public StudentValidation()
        {
            RuleFor(student => student.Name)
                .NotEmpty().WithMessage("Name cannot be null or empty.")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.")
                .WithMessage(ErrorMessageException.NAME_EMPTY);

            RuleFor(student => student.Email).NotEmpty().WithMessage(ErrorMessageException.EMAIL_EMPTY);
            RuleFor(student => student.CPF).NotEmpty().WithMessage(ErrorMessageException.CPF);
        }
    }
}