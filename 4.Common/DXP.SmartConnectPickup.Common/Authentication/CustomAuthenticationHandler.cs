using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.Common.Authentication
{
    /// <summary>
    /// Guideline: https://dotnetcorecentral.com/blog/authentication-handler-in-asp-net-core/
    /// https://referbruv.com/blog/posts/implementing-custom-authentication-scheme-and-handler-in-aspnet-core-3x
    /// </summary>
    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationOptions>
    {
        private readonly ILogger _logger;
        public static string Secret { get; set; }

        public CustomAuthenticationHandler(
            IOptionsMonitor<CustomAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
            _logger = logger.CreateLogger<CustomAuthenticationHandler>();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            try
            {
                return Task.FromResult(ValidateToken(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ValidateToken Failed");
                return Task.FromResult(AuthenticateResult.Fail(ex.Message));
            }
        }

        private AuthenticateResult ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            ClaimsIdentity identity = GetIdentityFromToken(token);
            identity.AddClaim(new Claim("AccessToken", token));

            GenericPrincipal principal = new GenericPrincipal(identity, null);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        private ClaimsIdentity GetIdentityFromToken(string token)
        {
            var tokenDecoder = new JwtSecurityTokenHandler();
            var jwtSecurityToken = (JwtSecurityToken)tokenDecoder.ReadToken(token);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            ClaimsPrincipal principal = tokenDecoder.ValidateToken(
                jwtSecurityToken.RawData,
                new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    RequireExpirationTime = false,
                    RequireSignedTokens = false,
                    IssuerSigningKey = securityKey,
                },
                out _);

            return principal.Identities.FirstOrDefault();
        }
    }
}
