using Ecommerce.Api.Models;
using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : Controller
    {
        private readonly FormService _formService;
        public FormController(FormService formService)
        {
            _formService = formService;
        }

        [HttpGet("GetForms")]
        public async Task<Response> GetForms() { 
            var result = await _formService.GetAllAsync();
            return new Response()
            {
                Data = result,
            };
        }

        [HttpGet("GetFormById/{id}")]
        public async Task<Response> GetFormById(int id)
        {
            var result = await _formService.GetByIdAsync(id);
            return new Response()
            {
                Data = result,
            };
        }
    }
}
