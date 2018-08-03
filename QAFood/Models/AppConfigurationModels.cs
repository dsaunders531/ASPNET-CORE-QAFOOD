using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.Models
{
    // REVIEW - too many classes in one file.

    /// <summary>
    /// The application configuration. Used by the AppConfiguration service.
    /// This class is in the same format as the appsettings.json file.
    /// </summary>
    [NotMapped]
    public class AppConfigurationModel
    {
        public ConnectionStringsConfigurationModel ConnectionStrings { get; set; }
        public DefaultsConfigurationModel Defaults { get; set; }
        public LoggingConfigurationModel Logging { get; set; }
        public SeedDataModel SeedData { get; set; }
    }

    [NotMapped]
    public class ConnectionStringsConfigurationModel
    {
        public string Content { get; set; }
        public string Authentication { get; set; }
    }

    [NotMapped]
    public class DefaultsConfigurationModel
    {
        public int ListItemsPerPage { get; set; }
    }

    [NotMapped]
    public class LoggingConfigurationModel
    {
        public bool StdOutEnabled { get; set; }
        public string StdOutLevel { get; set; }
        public bool LogXMLEnabled { get; set; }
        public string LogXMLPath { get; set; }
        public string LogXMLLevel { get; set; }
        public long LogRotateMaxEntries { get; set; }
    }

    [NotMapped]
    public class SeedDataModel
    {
        public List<string> CreateDefaultRoles { get; set; }
        public SeedDataUserModel AdminUser { get; set; }
        public string DefaultRole { get; set; }
    }

    [NotMapped]
    public class SeedDataUserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
