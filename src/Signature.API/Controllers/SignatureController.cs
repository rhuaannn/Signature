using Microsoft.AspNetCore.Mvc;
using Signature.Application.Interface;
using Signature.Application.ViewModels;
using Signature.Application.Mapping;

namespace Signature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly ISignature _signatureService;

        public SignatureController(ISignature signatureService)
        {
            _signatureService = signatureService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSignature([FromBody] CreateViewModelSignature viewModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var signature = viewModel.ToDomain();
            await _signatureService.CreateSignatureAsync(signature);

            return CreatedAtAction(nameof(GetById), new { id = signature.Id }, viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            return Ok();
        }
    }
}