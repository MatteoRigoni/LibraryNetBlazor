namespace Library.WebApi.Authentication
{
    public interface ICustomUserManager
    {
        string Authenticate(string username, string password);
    }
}