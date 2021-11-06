
namespace Library.UseCases
{
    public interface IAuthenticationUseCase
    {
        Task<string> GetUserInfo(string token);
        Task<string> Login(string username, string password);
        Task Logout();
    }
}