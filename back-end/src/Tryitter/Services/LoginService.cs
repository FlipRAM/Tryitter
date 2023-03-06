using SchoolLogin.Services;

public class LoginService {

    private readonly ITryitterRepository<Student> _repository;
    private readonly ILogger<LoginService> _logger;

    public LoginService(ITryitterRepository<Student> repository,
        ILogger<LoginService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<string> LoginStudent(LoginRequest login){

        var student = await _repository.GetByNameOrEmail(login.Email)!;
        
        if (student == null)
            throw new StudentNotFound("Estudante não encontrado ao efetuar login");

        if (student.Password != login.Password)
            throw new IncorrectPassword("Senha de estudante incorreta");
            
        student.Password = String.Empty;

        return new TokenGenerator().Generate(student);
    }
}