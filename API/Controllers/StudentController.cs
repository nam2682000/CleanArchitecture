using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Aplication.Features.Course.Command.Handdle;
using Web.Aplication.Features.Course.Command.Request;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        private readonly ILogger<StudentController> _logger;

        private readonly IMediator _mediator;
        public StudentController(ILogger<StudentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var studentDetails = await _mediator.Send(new GetStudentListQuery());
            return Ok(studentDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateStudentCommand request)
        {
            var studentDetails = await _mediator.Send(request);
            return Ok(studentDetails);
        }
    }
}
