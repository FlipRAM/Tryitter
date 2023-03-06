public class PostService {
    private readonly ITryitterRepository<Student> _repository;
    private readonly ILogger<PostService> _logger;

    public PostService(ITryitterRepository<Student> repository,
        ILogger<PostService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Post> CreatePost(PostRequest postRequest)
    {
        var post = new Post {
            Content = postRequest.Content,
            Student = postRequest.Student,
            ImageData = postRequest.ImageData,
            ImageMime = postRequest.ImageMime,
        };

        await _repository.Add(post);

        return post;
    }

    public async Task<IEnumerable<Post>> GetAllByStudentName(string StudentName)
    {
        var students = await _repository.GetByNameOrEmail(StudentName)!;

        if (students == null)
            throw new StudentNotFound($"Não há aluno cadastrado com nome {StudentName}");

        var studentPosts = await _repository.GetAllById(students.StudentId);

        if (studentPosts == null)
            throw new NoContent("Não há posts deste estudante");
        
        return studentPosts;
    }

    public async Task<Post> GetById(int id)
    {
        var post = await _repository.GetById<Post>(id);

        if (post == null)
            throw new StudentNotFound($"Não há post com o id {id}");
        
        return post;
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