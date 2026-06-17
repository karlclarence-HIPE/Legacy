namespace Legacy.Migrator.Configuration;

public class DbMigratorConfiguration
{
    public required string ConnectionString { get; set; }

    public required string DatabaseName { get; set; }
}
