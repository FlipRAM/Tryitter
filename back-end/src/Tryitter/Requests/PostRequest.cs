using System.ComponentModel.DataAnnotations;
public class PostRequest
{
    [MinLength(1)]
    public string Content { get; set; } = default!;
    public byte[]? ImageData { get; set; }
    public string? ImageMime { get; set; }
    public Student Student { get; set; }
}