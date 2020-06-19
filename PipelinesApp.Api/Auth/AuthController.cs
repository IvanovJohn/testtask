namespace PipelinesApp.Api.Auth
{
    using System;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Authentication controller.
    /// </summary>
    [ApiController]
    [Route("api/token")]
    public class AuthController : ControllerBase
    {
        private const string AuthorizationHeaderName = "Authorization";
        private const string BasicSchemeName = "Basic";

        private readonly IUserAuthenticationService authenticationService;

        /// <summary>
        ///  Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authenticationService"><see cref="IUserAuthenticationService"/>.</param>
        public AuthController(IUserAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="form"><see cref="AuthenticateForm"/>.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(AuthenticateForm form)
        {
            if (string.IsNullOrEmpty(form.User))
            {
                this.TryToUseBasicAuth(form);
            }

            var token = await this.authenticationService.Authenticate(form.User, form.Password);

            if (token == null)
            {
                return this.BadRequest(new { message = "User or password is incorrect" });
            }

            return this.Ok(new AuthResult { Token = token });
        }

        private void TryToUseBasicAuth(AuthenticateForm form)
        {
            if (!AuthenticationHeaderValue.TryParse(this.Request.Headers[AuthorizationHeaderName], out var headerValue))
            {
                // Invalid Authorization header
                return;
            }

            if (!BasicSchemeName.Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                // Not Basic authentication header
                return;
            }

            byte[] headerValueBytes = Convert.FromBase64String(headerValue.Parameter);
            string userAndPassword = Encoding.UTF8.GetString(headerValueBytes);
            string[] parts = userAndPassword.Split(':');
            if (parts.Length != 2)
            {
                return;
            }

            form.User = parts[0];
            form.Password = parts[1];
        }
    }
}