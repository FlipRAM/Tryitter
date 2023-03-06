using Microsoft.AspNetCore.Mvc;

namespace Tryitter.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _service;
    private readonly ILogger<LoginController> _logger;

    public LoginController(LoginService service, ILogger<LoginController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] LoginRequest student)
    {
        var token = await _service.LoginStudent(student);
        _logger.LogInformation(student.ToString());
        return Ok(token);
    }
}