using IdentityServer4.Models;

namespace Server
{
    public class Config
    {
        //This defines the identity resources that the identity server can provide
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            };

        //These are the scopes that the APIs will use to protect their resources
        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("CoffeeAPI.read"), new ApiScope("CoffeeAPI.write"), };

        //This defines the APIs that the identity server protects
        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("CoffeeAPI")
                {
                    Scopes = new List<string> { "CoffeeAPI.read", "CoffeeAPI.write" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            };

        //These are the clients that are allowed to request tokens from the identity server
        public static IEnumerable<Client> Clients =>
            /*
            ClientId: Unique identifier for the client.
            ClientSecrets: Secret used to authenticate the client.
            AllowedGrantTypes: Types of grants allowed for the client (e.g., ClientCredentials, Code).
            RedirectUris: URIs to redirect to after a successful login.
            FrontChannelLogoutUri: URI for front-channel logout.
            PostLogoutRedirectUris: URIs to redirect to after logout.
            AllowOfflineAccess: Indicates whether the client can request refresh tokens.
            AllowedScopes: Scopes that the client is allowed to request.
            RequirePkce: Indicates whether PKCE is required for the client.
            RequireConsent: Indicates whether the user consent is required.
            AllowPlainTextPkce: Indicates whether plain text PKCE is allowed (should be false for security).
            */
            new[]
            {
                //m2m (machine-to-machine) client with credentials flow
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedScopes = { "CoffeeAPI.read", "CoffeeAPI.write" }
                },

                //interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5444/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "CoffeeAPI.read" },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                },
            };
    }
}
