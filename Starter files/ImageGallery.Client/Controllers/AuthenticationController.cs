using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using static Duende.IdentityServer.Models.IdentityResources;

namespace ImageGallery.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        [Authorize]
        public async Task Logout()
        {

            var client = _httpClientFactory.CreateClient("IDPClient");
            var discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync();
            if (discoveryDocumentResponse.IsError)
            {
                throw new Exception(discoveryDocumentResponse.Error);
            }
            var revocateToken = async (string name) =>
            {
                var request = new TokenRevocationRequest 
                {
                    Address = discoveryDocumentResponse.RevocationEndpoint,
                    ClientId = "imagegalleryclient",
                    ClientSecret = "secret",
                    Token = await HttpContext.GetTokenAsync(name)
                };
                var result = await client.RevokeTokenAsync(request);
                if (result.IsError)
                {
                    throw new Exception(result.Error);
                }
                return result;
            };
            var acccessTokenRevocationResponse = await revocateToken(OpenIdConnectParameterNames.AccessToken);
            var refreshTokenRevocationResponse = await revocateToken(OpenIdConnectParameterNames.RefreshToken);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);  //let him clear his cookie too.
        }
        [Authorize]
        public IActionResult AccessDenied()
        {


            return View();
        }
        public AuthenticationController (IHttpClientFactory httpClientFactory )
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
    }
}
