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

    [HttpGet("Student/{name}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPostsByStudent(string name)
    {
        var response = await _service.GetAllByStudentName(name);

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
    public async Task<ActionResult<Student>> CreatePost([FromBody] PostRequest post)
    {
        var response = await _service.CreatePost(post);

        return Ok(response);
    }
}