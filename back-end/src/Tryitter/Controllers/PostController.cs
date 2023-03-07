using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tryitter.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly PostService _service;
    private readonly ILogger<PostController> _logger;

    public PostController(PostService service, ILogger<PostController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("User/{name}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPostsByUser(string name)
    {
        var response = await _service.GetAllByUsername(name);

        return Ok(response);
    }

    [HttpGet("{id}")]
    //[Authorize]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        var response = await _service.GetById(id);

        return Ok(response);
    }

    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<User>> CreatePost([FromBody] PostRequest post)
    {
        var response = await _service.CreatePost(post);

        return Ok(response);
    }

    [HttpPut("{id}")]
    // [Authorize(Policy = "EditProfile")]
    public async Task<ActionResult<User>> UpdatePost(int id, [FromBody] PostRequest post)
    {
        await _service.UpdatePost(id, post);

        return NoContent();
    }

    [HttpDelete("{id}")]
    // [Authorize(Policy = "EditProfile")]
    public async Task<ActionResult<User>> ExcludePost(int id)
    {
        await _service.DeletePost(id);

        return NoContent();
    }
}