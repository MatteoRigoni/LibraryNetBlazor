namespace Library.WebApi.Authentication
{
    public class CustomTokenManager : ICustomTokenManager
    {
        private List<Token> tokens = new List<Token>();

        public string CreateToken(string username)
        {
            var token = new Token(username);
            tokens.Add(token);

            return token.TokenString;
        }

        public bool VerifyToken(string tokenString)
        {
            return tokens.Any(x => tokenString != null
                && tokenString.Contains(x.TokenString)
                && x.ExpireAt > DateTime.Now);
        }

        public string GetUserInfoByToken(string tokenString)
        {
            var token = tokens.FirstOrDefault(x => tokenString != null && tokenString.Contains(x.TokenString));
            if (token != null) return token.Username;

            return String.Empty;
        }
    }
}
