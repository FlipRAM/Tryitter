public class StudentService {
    private readonly ITryitterRepository<Student> _repository;
    private readonly ILogger<StudentService> _logger;

    public StudentService(ITryitterRepository<Student> repository,
        ILogger<StudentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Student> CreateStudent(StudentRequest studentRequest)
    {
        var student = new Student {
            Email = studentRequest.Email,
            Password = studentRequest.Password,
            StudentName = studentRequest.StudentName,
            Status = studentRequest.Status,
            CurrentModule = studentRequest.CurrentModule
        };

        await _repository.Add(student);

        student.Password = String.Empty;

        return student;
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        var students = await _repository.GetAll<Student>();

        if (students == null)
            throw new NoContent("Não há alunos cadastrados");

        var serializedStudents = students.Select(s => new Student {
            StudentId = s.StudentId,
            StudentName = s.StudentName,
            CurrentModule = s.CurrentModule,
            Email = s.Email,
            Status = s.Status,
        });
        
        return serializedStudents;
    }

    public async Task<Student> GetById(int id)
    {
        var student = await _repository.GetById<Student>(id);

        if (student == null)
            throw new StudentNotFound($"Não há aluno com o id {id}");

        student.Password = String.Empty;
        
        return student;
    }

    public async Task UpdateStudent(int id, UpdateStudentRequest studentupdate)
    {
        var student = await _repository.GetById<Student>(id);

        if (student == null)
            throw new StudentNotFound($"Não há aluno com o id {id}");
        
        if (studentupdate.StudentName != null)
            student.StudentName = studentupdate.StudentName;
        if (studentupdate.Status != null)
            student.Status = studentupdate.Status;
        if (studentupdate.CurrentModule != null)
            student.CurrentModule = (Modules)studentupdate.CurrentModule;
        if (studentupdate.Password != null)
            student.Password = studentupdate.Password;

        await _repository.Update(student);
    }

    public async Task DeleteStudent(int id)
    {
        var student = await _repository.GetById<Student>(id);

        if (student == null)
            throw new StudentNotFound($"Não há aluno com o id {id}");
        
        await _repository.Delete(student);
    }
}