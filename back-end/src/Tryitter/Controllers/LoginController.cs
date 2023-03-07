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
    public async Task<ActionResult<string>> Login([FromBody] LoginRequest user)
    {
        var token = await _service.LoginUser(user);
        _logger.LogInformation(user.ToString());
        return Ok(token);
    }
}