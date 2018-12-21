namespace DIM_Vision_Cross
{
    public class VisionConstants
    {
        #region INFRAESTRUCTURE STRINGS
        public const string DefaultConnectionString = "DefaultConnection";
        public const string AzureVisionKey = "AzureVisionKey";
        public const string AzureVisionApi = "AzureVisionApi";
        public const string GoogleAppCredentials = "GoogleAppCredentials";
        public const string GoogleAppKey = "GOOGLE_APPLICATION_CREDENTIALS";
        public static readonly string[] GrammasSettings = new string[] {
            "CFGConfidenceRejectionThreshold",
            "HighConfidenceThreshold",
            "NormalConfidenceThreshold",
            "LowConfidenceThreshold"
        };
        #endregion
    }
}
