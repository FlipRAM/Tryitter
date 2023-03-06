using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tryitter.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _service;
    private readonly ILogger<StudentController> _logger;

    public StudentController(StudentService service, ILogger<StudentController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
    {
        var response = await _service.GetAll();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudentById(int id)
    {
        var response = await _service.GetById(id);

        return Ok(response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Student>> PostStudent([FromBody] StudentRequest student)
    {
        var response = await _service.CreateStudent(student);

        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "EditProfile")]
    public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] UpdateStudentRequest student)
    {
        await _service.UpdateStudent(id, student);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "EditProfile")]
    public async Task<ActionResult<Student>> ExcludeStudent(int id)
    {
        await _service.DeleteStudent(id);

        return NoContent();
    }
}