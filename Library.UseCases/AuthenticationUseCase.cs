using Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UseCases
{
    public class AuthenticationUseCase : IAuthenticationUseCase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationUseCase(IAuthenticationRepository authenticationRepository, ITokenRepository tokenRepository)
        {
            _authenticationRepository = authenticationRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> Login(string username, string password)
        {
            return await _authenticationRepository.Login(username, password);
        }

        public async Task<string> GetUserInfo(string token)
        {
            return await _authenticationRepository.GetUserInfo(token);
        }

        public async Task Logout()
        {
            await _tokenRepository.SetToken(String.Empty);
        }
    }
}
