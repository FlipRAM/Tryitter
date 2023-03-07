using SchoolLogin.Services;

public class LoginService {

    private readonly ITryitterRepository<User> _repository;
    private readonly ILogger<LoginService> _logger;

    public LoginService(ITryitterRepository<User> repository,
        ILogger<LoginService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<string> LoginUser(LoginRequest login){

        var user = await _repository.GetByNameOrEmail(login.Email)!;
        
        if (user == null)
            throw new UserNotFound("User not found to log in");

        if (user.Password != login.Password)
            throw new IncorrectPassword("Incorrect Password");
            
        user.Password = String.Empty;

        return new TokenGenerator().Generate(user);
    }
}