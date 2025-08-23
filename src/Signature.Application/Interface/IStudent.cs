using Signature.Application.ViewModels;
using Signature.Domain.Entities;

namespace Signature.Application.Interface
{
    public interface IStudent
    {
        Task<Student> CreateStudentAsync(Student student);
        Task<List<Student>> GetAllStudentAsync();


    }
}
