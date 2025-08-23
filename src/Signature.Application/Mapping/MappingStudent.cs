namespace Signature.Application.Mapping
{
    public static class MappingStudent
    {
        public static Domain.Entities.Student ToDomain(this ViewModels.CreateStudentViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            return new Domain.Entities.Student(viewModel.Name, viewModel.Email, viewModel.CPF);
        }
        public static ViewModels.CreateStudentViewModel ToViewModel(this Domain.Entities.Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            return new ViewModels.CreateStudentViewModel(student.Name, student.Email.Value, student.CPF.Value);
        }

    }
}
