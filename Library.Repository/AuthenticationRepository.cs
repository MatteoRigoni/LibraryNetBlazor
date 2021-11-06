using Library.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationRepository(IWebApiExecuter webApiExecuter, ITokenRepository tokenRepository)
        {
            _webApiExecuter = webApiExecuter;
            _tokenRepository = tokenRepository;
        }
        public async Task<string> Login(string username, string password)
        {
            var token = await this._webApiExecuter.InvokePostAsString("authenticate"
                , new { username = username, password = password });

            await _tokenRepository.SetToken(token);
            return token;
        }

        public async Task<string> GetUserInfo(string token)
        {
            return await this._webApiExecuter.InvokePostAsString("getuserinfo"
                , new { token = token });
        }
    }
}
