using byr.IDP;
using ImageGallery.Authorization;
using ImageGallery.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(configure => 
        configure.JsonSerializerOptions.PropertyNamingPolicy = null);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // get rid of the (default, legacy ) mapping used for 'claims' normally stored in th token

builder.Services.AddAccessTokenManagement();


builder.Services
    // create an HttpClient used for accessing the API
    .AddHttpClient("APIClient", client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["ImageGalleryAPIRoot"]);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    })
    // include a handler which will add the access token to each outgoing request.  We got this one cheap from NUGET IdentityModel.AspNetCore
    .AddUserAccessTokenHandler() ;

builder.Services
    // create an HttpClient 
    .AddHttpClient("IDPClient", client =>
    {
        client.BaseAddress = new Uri("https://localhost:5001");
    })
    // include a handler which will add the access token to each outgoing request.  We got this one cheap from NUGET IdentityModel.AspNetCore
    .AddUserAccessTokenHandler();


builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.AccessDeniedPath = "/Authentication/AccessDenied";
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = "https://localhost:5001/"; //
        options.ClientId = "imagegalleryclient";   //we defined these in our Config.Client class in our IDP
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        //options.Scope.Add("openid");              //these too, so he must be able top work them out
        //options.Scope.Add("profile");
        options.Scope.Add("roles");
        //options.Scope.Add(Config.allowFull);  // superceded by introduction of claims policies
        options.Scope.Add(Config.allowRead);
        options.Scope.Add(Config.allowWrite);
        options.Scope.Add("country");
        options.Scope.Add("offline_access");
        //options.CallbackPath = new PathString("signin-oidc");
        // the default here is SignedOutCallbackPath: port/signout-callback-oidc
        // options.SignedOutCallbackPath= "pathAfterSignout";
        options.SaveTokens = true; // tokens will be saved in a cookie
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClaimActions.Remove("aud");  // audience, will still be there but without default mapping
        options.ClaimActions.DeleteClaim("sid");   // session id
        options.ClaimActions.DeleteClaim("idp");   // identityProvider
        options.ClaimActions.MapJsonKey("role", "role");
        options.TokenValidationParameters = new()
        { NameClaimType = "given_name", RoleClaimType = "role" };
        options.ClaimActions.MapUniqueJsonKey("country", "country");
        ////???????????????  here's some really exciting stuff   STUB?????????????????
        //options.BackchannelHttpHandler = new HttpClientHandler
        //{
        //    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        //};
    })
    .Services.AddAuthorization(options =>       // first steps with an Authorization Policy
    {
        options.AddPolicy(Names.ImageAdd, AuthorizationPolicies.CanAddImage());
    })
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gallery}/{action=Index}/{id?}");

app.Run();
