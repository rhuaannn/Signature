using Signature.Application.Interface;
using Signature.Application.Mapping;
using Signature.Application.ViewModels;
using Signature.Domain.Entities;
using Signature.Exception.Exception;
using Signature.Infra.ContextDB;
using Signature.Infra.Interface;
using Signature.Infra.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Signature.Application.Services
{
    public class ServiceStudent : IStudent
    {
        private readonly IStudentRepository _studentRepository;
        public ServiceStudent(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;

        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            Validate(student);

            var existingStudentCPF = await _studentRepository.GetByCPFAsync(student.CPF);
            if (existingStudentCPF != null)
            {
                throw new ArgumentException("A student with this CPF already exists.", nameof(student.CPF.Value));
            }

            var createStudent = await _studentRepository.CreateAsync(student);
            return student;
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            var student = await _studentRepository.GetAllStudentAsync();
            return student;
        }


        private void Validate(Student student)
        {
            var validator = new StudentValidation();
            var result = validator.Validate(student);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErroOnValidationException(errors);
            }
        }
    }
}
