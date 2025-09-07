using Microsoft.EntityFrameworkCore;
using Signature.Domain.Entities;
using Signature.Domain.ValueObjects;
using Signature.Infra.ContextDB;
using Signature.Infra.Interface;

namespace Signature.Infra.Repositories
{
    public class Repository : IStudentRepository, ISignatureRepository
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

        public async Task<Domain.Entities.Signature> CreateSignatureAsync(Domain.Entities.Signature signature)
        {
            if (signature == null)
            {
                throw new ArgumentNullException(nameof(signature), "Signature cannot be null");
            }
            await _connection.Signatures.AddAsync(signature);
            await _connection.SaveChangesAsync();
            return signature;
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

        public Task<Student> GetByCPFAsync(CPF cpf)
        {
            var studentCPF = _connection.Students.FirstOrDefaultAsync(s => s.CPF.Value == cpf);
            if (studentCPF == null)
            {
                throw new ArgumentException("Student not found");
            }
            return studentCPF;
        }



        public Task<Student> UpdateAsync(Student student)
        {
            throw new NotImplementedException();
        }


    }
}
