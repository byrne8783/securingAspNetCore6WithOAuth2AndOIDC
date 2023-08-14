using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace byr.IDP;
/// <summary>
/// A config class with stuff we're going to stuff into the IdentityServer service
/// </summary>
public static class Config
{
    const string appFamily = "imagegallery";
    const string appAPI = $"{appFamily}api";
    const string appClient = $"{appFamily}client";
    public const string allowFull = $"{appAPI}.fullaccess";
    public const string allowRead = $"{appAPI}.read";
    public const string allowWrite = $"{appAPI}.write";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            // these two are there by default and map to scopes of the same name
            new IdentityResources.OpenId()
            ,new IdentityResources.Profile()
            // these are not.  Do remember in addition to being defined, it also has to be allowed
            ,new IdentityResource("roles","Your role(s)",new [] { "role" })  // that first parameter is a scope
            ,new IdentityResource("country","Your Country of Residence",new List<string>() { "country" })
        };
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            //this adds the role claim to the Access Token
            new ApiResource(appAPI,"Image Gallery API",new [] { "role","country"})
            {
                Scopes = { allowFull, allowRead, allowWrite }
                ,ApiSecrets = {new Secret("apisecret".Sha256()) }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope(allowFull)
                ,new ApiScope(allowRead)
                ,new ApiScope(allowWrite )
            };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
                ClientName = "Image Gallery"
               , ClientId = appClient
               , AllowedGrantTypes = GrantTypes.Code
               //, AuthorizationCodeLifetime  = ..5 mins default..
               //, IdentityTokenLifetime =   = ..5 mins default..
               , AllowOfflineAccess = true     // provides refresh token support
               , AccessTokenLifetime = 120
                //,AbsoluteRefreshTokenLifetime = 2592000 the default value is OK
               //, RefreshTokenExpiration = ..rarelyUsed'''
               //, SlidingRefreshTokenLifetime =              // 15 days is default
               , UpdateAccessTokenClaimsOnRefresh = true     // when some of the claims are changed, sort out the token implications  
                ,AccessTokenType = AccessTokenType.Reference // add reference tokend
               , RedirectUris = new HashSet<string>(){ "https://localhost:7184/signin-oidc" }
               , PostLogoutRedirectUris = new HashSet<string>(){ "https://localhost:7184/signout-callback-oidc" }
               , AllowedScopes =                                        //what is the client allowed to request
                {
                    IdentityServerConstants.StandardScopes.OpenId
                    , IdentityServerConstants.StandardScopes.Profile
                    ,"roles"
                    //,"imagegalleryapi"  superceded by introduction of resources and underlying scopes
                    ,"country"
                    //,allowFull superceded by introduction of policies as scopes
                    ,allowRead
                    ,allowWrite
                }
               , ClientSecrets = {  new Secret("secret".Sha256()) }
                ,RequireConsent = true   //false by default
            }
        };


}