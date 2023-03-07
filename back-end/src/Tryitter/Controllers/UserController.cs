using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tryitter.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _service;
    private readonly ILogger<UserController> _logger;

    public UserController(UserService service, ILogger<UserController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var response = await _service.GetAll();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<User>>> GetUserById(int id)
    {
        var response = await _service.GetById(id);

        return Ok(response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<User>> PostUser([FromBody] UserRequest user)
    {
        var response = await _service.CreateUser(user);

        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "EditProfile")]
    public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] UpdateUserRequest user)
    {
        await _service.UpdateUser(id, user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "EditProfile")]
    public async Task<ActionResult<User>> ExcludeUser(int id)
    {
        await _service.DeleteUser(id);

        return NoContent();
    }
}