using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signature.Application.Interface;
using Signature.Application.Mapping;
using Signature.Application.ViewModels;
using Signature.Domain.Entities;

namespace Signature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _studentService;

        public StudentController(IStudent studentService)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStudentViewModel viewModel)
        {

            var student = viewModel.ToDomain();

            var createStudent = await _studentService.CreateStudentAsync(student);

            var responseViewModel = createStudent.ToViewModel();

            return Created("Sucesso", responseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllStudentAsync();

            if (students == null || !students.Any())
            {
                return NotFound("No students found.");
            }

            var responseViewModel = students.Select(s => s.ToViewModel()).ToList();

            return Ok(responseViewModel);
        }
    }
}