using DotNet.CleanArch.Template.Domain.Common.Results;

namespace DotNet.CleanArch.Template.Domain.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<string>> GetUserNameAsync(int userId);
        Task<Result<bool>> IsInRoleAsync(int userId, string role);
        Task<Result<bool>> AuthorizeAsync(int userId, string policyName);
        Task<Result<int>> CreateUserAsync(string firstName, string lastName, string email, string password);
        Task<Result<string>> AuthenticateAsync(string email, string password);
        Task<Result> DeleteUserAsync(int userId);
    }
}
