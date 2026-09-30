namespace DiGi.WebAPI.WindowsService.Constants
{
    /// <summary>
    /// Provides a centralized location for naming constants used throughout the Windows Service application.
    /// </summary>
    public static class Name
    {
        /// <summary>
        /// The unique identifier or display name of the Windows Service.
        /// </summary>
        public const string Service = "DiGi.WebAPI.WindowsService";

        /// <summary>
        /// The name of the policy used for managing subdomains.
        /// </summary>
        public const string Policy = "DiGi_Subdomains_Policy";

        /// <summary>
        /// The name of the Swagger document that includes every endpoint, regardless of its route prefix.
        /// <para>Served at <c>/swagger/full/swagger.json</c> and, through a rewrite, at <c>/swagger/swagger.json</c>. No route prefix may use this name.</para>
        /// </summary>
        public const string SwaggerDocument_Full = "full";
    }
}