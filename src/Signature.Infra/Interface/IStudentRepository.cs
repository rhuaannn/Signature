using Signature.Domain.Entities;

namespace Signature.Infra.Interface
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllStudentAsync();
        public Task<Student> CreateAsync(Student student);
        public Task<Student> UpdateAsync(Student student);
        public Task DeleteAsync(Guid studentId);
        public Task<Student> GetByCPFAsync(string cpf);
    }
}
