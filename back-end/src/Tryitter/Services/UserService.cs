public class UserService {
    private readonly ITryitterRepository<User> _repository;
    private readonly ILogger<UserService> _logger;

    public UserService(ITryitterRepository<User> repository,
        ILogger<UserService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<User> CreateUser(UserRequest userRequest)
    {
        var user = new User {
            Email = userRequest.Email,
            Password = userRequest.Password,
            Username = userRequest.Username,
            Status = userRequest.Status,
        };

        await _repository.Add(user);

        user.Password = String.Empty;

        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        var users = await _repository.GetAll<User>();

        if (users == null)
            throw new NoContent("There is no user registered");

        var serializedUsers = users.Select(s => new User {
            UserId = s.UserId,
            Username = s.Username,
            Email = s.Email,
            Status = s.Status,
        });
        
        return serializedUsers;
    }

    public async Task<User> GetById(int id)
    {
        var user = await _repository.GetById<User>(id);

        if (user == null)
            throw new UserNotFound($"There is no user with id {id}");

        user.Password = String.Empty;
        
        return user;
    }

    public async Task UpdateUser(int id, UpdateUserRequest userUpdate)
    {
        var user = await _repository.GetById<User>(id);

        if (user == null)
            throw new UserNotFound($"There is no user with id {id}");
        
        if (userUpdate.Username != null)
            user.Username = userUpdate.Username;
        if (userUpdate.Status != null)
            user.Status = userUpdate.Status;
        if (userUpdate.Password != null)
            user.Password = userUpdate.Password;

        await _repository.Update(user);
    }

    public async Task DeleteUser(int id)
    {
        var user = await _repository.GetById<User>(id);

        if (user == null)
            throw new UserNotFound($"There is no user with id {id}");
        
        await _repository.Delete(user);
    }
}