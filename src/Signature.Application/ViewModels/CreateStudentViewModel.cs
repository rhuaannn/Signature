using System.ComponentModel.DataAnnotations;

namespace Signature.Application.ViewModels
{
    public class CreateStudentViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "CPF is required.")]
        public string CPF { get; set; }
        public CreateStudentViewModel() { }
        public CreateStudentViewModel(string name, string email, string cpf)
        {
            Name = name;
            Email = email;
            CPF = cpf;
        }
    }
}
