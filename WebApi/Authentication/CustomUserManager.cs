namespace Library.WebApi.Authentication
{
    public class CustomUserManager : ICustomUserManager
    {
        private Dictionary<string, string> credentials = new Dictionary<string, string>()
        {
            { "user1", "password1" },
            { "user2", "password2" }
        };

        private readonly ICustomTokenManager _customTokenManager;

        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            _customTokenManager = customTokenManager;
        }
        public string Authenticate(string username, string password)
        {
            if (credentials[username] != password) return string.Empty;

            return _customTokenManager.CreateToken(username);
        }

        
    }
}
