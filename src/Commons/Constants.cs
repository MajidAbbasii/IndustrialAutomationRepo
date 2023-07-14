namespace Commons;

public static class Constants
{
    public static class Defaults
    {
        public static string StartDate = "0000/00/00";
        public static string EndDate = "9999/99/99";
    }
    
    public class UserSecrets
    {
        public const string InitialCatalog = nameof(InitialCatalog);
        public const string PersistSecurityInfo = nameof(PersistSecurityInfo);
        public const string DataSource = nameof(DataSource);
        public const string TrustServerCertificate = nameof(TrustServerCertificate);
        public const string IntegratedSecurity = nameof(IntegratedSecurity);
    }
    
    public class AppSetting
    {
        public const string Configuration = nameof(Configuration);
    }

    
}