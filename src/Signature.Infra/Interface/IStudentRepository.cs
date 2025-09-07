using Signature.Domain.Entities;
using Signature.Domain.ValueObjects;

namespace Signature.Infra.Interface
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllStudentAsync();
        public Task<Student> CreateAsync(Student student);
        public Task<Student> UpdateAsync(Student student);
        public Task DeleteAsync(Guid studentId);
        public Task<Student> GetByCPFAsync(CPF cpf);
    }
}
