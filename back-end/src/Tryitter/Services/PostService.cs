public class PostService {
    private readonly ITryitterRepository<User> _repository;
    private readonly ILogger<PostService> _logger;

    public PostService(ITryitterRepository<User> repository,
        ILogger<PostService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Post> CreatePost(PostRequest postRequest)
    {
        var post = new Post {
            Content = postRequest.Content,
            UserId = postRequest.UserId,
        };

        await _repository.Add(post);

        return post;
    }

    public async Task<IEnumerable<Post>> GetAllByUsername(string UserName)
    {
        var users = await _repository.GetByNameOrEmail(UserName)!;

        if (users == null)
            throw new UserNotFound($"There is no user {UserName}");

        var userPosts = await _repository.GetAllById(users.UserId);

        if (userPosts == null)
            throw new NoContent("There is no posts from this user");
        
        return userPosts;
    }

    public async Task<Post> GetById(int id)
    {
        var post = await _repository.GetById<Post>(id);

        if (post == null)
            throw new UserNotFound($"There is no posts with id {id}");
        
        return post;
    }

    public async Task UpdatePost(int id, PostRequest postUpdate)
    {
        var post = await _repository.GetById<Post>(id);

        if (post == null)
            throw new PostNotFound($"There is no post with id {id}");
        
        if (postUpdate.Content != null)
            post.Content = postUpdate.Content;

        await _repository.Update(post);
    }

    public async Task DeletePost(int id)
    {
        var post = await _repository.GetById<Post>(id);

        if (post == null)
            throw new PostNotFound($"There is no post with id {id}");
        
        await _repository.Delete(post);
    }
}