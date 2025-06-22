namespace DotNet.CleanArch.Template.WebApi.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string[]? Errors { get; set; }
    public string? Details { get; set; }
}
