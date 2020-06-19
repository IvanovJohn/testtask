namespace PipelinesApp.Api.Auth
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Authentication;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.IdentityModel.Tokens;

    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.Users;
    using PipelinesApp.Api.Users.Queries;

    /// <summary>
    /// Authentication service.
    /// </summary>
    public interface IUserAuthenticationService
    {
        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="username">User.</param>
        /// <param name="password">Password.</param>
        /// <returns>Token.</returns>
        Task<string> Authenticate(string username, string password);
    }

    internal class UserAuthenticationService : IUserAuthenticationService
    {
#pragma warning disable SA1401 // Fields should be private
        public static byte[] TokenSecret = Encoding.ASCII.GetBytes("secretsecretsecretsecretsecretsecretsecretsecretsecretsecretsecretsecretsecretsecret");
#pragma warning restore SA1401 // Fields should be private

        private readonly IQueryDispatcher queryDispatcher;

        public UserAuthenticationService(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var user = await this.queryDispatcher.Ask<UserByNameCriterion, UserDbEntity>(new UserByNameCriterion { Name = username });
            if (user == null)
            {
                throw new AuthenticationException("Incorrect user or password");
            }

            // TODO add check password

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
                             {
                                 new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                                 new Claim(UserClaimsExtensions.UserIdClaimName, user.Id),
                             };
            var token =
                new JwtSecurityToken(
                    notBefore: DateTime.Now,
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(TokenSecret), SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(token);
        }
    }
}