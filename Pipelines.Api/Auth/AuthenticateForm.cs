namespace Pipelines.Api.Auth
{
    /// <summary>
    /// Authenticate model.
    /// </summary>
    public class AuthenticateForm
    {
        /// <summary>
        /// Gets or sets username.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        public string Password { get; set; }
    }
}