using Library.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Library.WebApp
{
    public class CustomTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public CustomTokenAuthenticationStateProvider(ITokenRepository tokenRepository,
            IAuthenticationRepository authenticationRepository)
        {
            _tokenRepository = tokenRepository;
            _authenticationRepository = authenticationRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenRepository.GetToken();
            var username = token != null ? await _authenticationRepository.GetUserInfo(await _tokenRepository.GetToken()): null;
            if (!String.IsNullOrWhiteSpace(username))
            {
                var claim = new Claim(ClaimTypes.Name, username);
                var identity = new ClaimsIdentity(new[] { claim }, "custom token auth");
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}
