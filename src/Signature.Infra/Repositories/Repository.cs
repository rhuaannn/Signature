using Microsoft.EntityFrameworkCore;
using Signature.Domain.Entities;
using Signature.Infra.ContextDB;
using Signature.Infra.Interface;

namespace Signature.Infra.Repositories
{
    public class Repository : IStudentRepository
    {
        private readonly Connection _connection;
        public Repository(Connection connection)
        {
            _connection = connection;
        }
        public async Task<Student> CreateAsync(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null");
            }
            await _connection.Students.AddAsync(student);
            await _connection.SaveChangesAsync();
            return student;
        }

        public async Task DeleteAsync(Guid studentId)
        {
            var student = await _connection.Students.FindAsync(studentId);
            if (student == null)
            {
                throw new ArgumentException("Student not found");

            }
            _connection.Students.Remove(student);
            await _connection.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            var studentExist = await _connection.Students.ToListAsync();
            if (studentExist == null)
            {
                throw new ArgumentException("Students not found");
            }
            return studentExist;
        }

        public Task<Student> GetByCPFAsync(string cpf)
        {
            var student = _connection.Students.FirstOrDefaultAsync(s => s.CPF.Value == cpf);
            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }
            return student;
        }

        public Task<Student> UpdateAsync(Student student)
        {
            throw new NotImplementedException();
        }


    }
}
