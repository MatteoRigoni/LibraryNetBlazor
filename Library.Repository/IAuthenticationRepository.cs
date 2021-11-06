
namespace Library.Repository
{
    public interface IAuthenticationRepository
    {
        Task<string> GetUserInfo(string token);
        Task<string> Login(string username, string password);
    }
}