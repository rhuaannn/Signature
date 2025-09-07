using Signature.Application.ViewModels;
using Signature.Domain.Entities;
using Signature.Domain.ValueObjects;

namespace Signature.Application.Interface
{
    public interface IStudent
    {
        Task<Student> CreateStudentAsync(Student student);
        Task<List<Student>> GetAllStudentAsync();
        Task<Student> GetByCPFAsync(CPF cpf);

    }
}
