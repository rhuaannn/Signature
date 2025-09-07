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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllStudentAsync();
            var responseViewModel = students.Select(s => s.ToViewModel()).ToList();

            return Ok(responseViewModel);
        }
        [HttpGet]
        [Route("cpf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCPF([FromQuery] string cpf)
        {

            var student = await _studentService.GetByCPFAsync(new Domain.ValueObjects.CPF(cpf));
            var responseViewModel = student.ToViewModel();
            return Ok(responseViewModel);
        }
    }
}