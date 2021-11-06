namespace Library.WebApi.Authentication
{
    public class Token
    {
        public Token(string username)
        {
            Username = username;
            TokenString = Guid.NewGuid().ToString();
            this.ExpireAt = DateTime.Now.AddMinutes(1);
        }
        public string TokenString { get; set; }
        public string Username { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}