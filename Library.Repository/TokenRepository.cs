using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IJSRuntime _iJSRuntime;

        public TokenRepository(IJSRuntime iJSRuntime)
        {
            _iJSRuntime = iJSRuntime;
        }
        public string Token { get; set; }

        public async Task SetToken(string token)
        {
            await _iJSRuntime.InvokeVoidAsync("sessionStorage.setItem", "token", token);
        }

        public async Task<string> GetToken()
        {
            return await _iJSRuntime.InvokeAsync<string>("sessionStorage.getItem", "token");
        }
    }
}
