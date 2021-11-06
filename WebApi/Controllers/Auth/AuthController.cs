using Library.WebApi.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers.Auth
{
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly ICustomUserManager _customerUserManager;
        private readonly ICustomTokenManager _customTokenManager;

        public AuthController(ICustomUserManager customerUserManager, ICustomTokenManager customTokenManager)
        {
            _customerUserManager = customerUserManager;
            _customTokenManager = customTokenManager;
        }

        [HttpPost]
        [Route("authenticate")]
        public Task<string> AuthenticateAsync(UserCredential userCredentials)
        {
            return Task.FromResult<string>(_customerUserManager.Authenticate(userCredentials.username, userCredentials.password));
        }

        [HttpGet]
        [Route("verifytoken")]
        public Task<bool> VerifyAsync(string token)
        {
            return Task.FromResult<bool>(_customTokenManager.VerifyToken(token));
        }

        [HttpPost]
        [Route("getuserinfo")]
        public Task<string> GetUserInfoByTokenAsync(TokenCredential tokenCred)
        {
            return Task.FromResult<string>(_customTokenManager.GetUserInfoByToken(tokenCred.token));
        }
    }

    public class TokenCredential
    {
        public string token { get; set; }
    }

    public class UserCredential
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
