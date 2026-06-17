using System.Reflection;
using Legacy.Migrator.Configuration;
using DbUp;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("settings.json", optional: false, reloadOnChange: true)
    .Build();

var settings = configuration.GetRequiredSection("Settings").Get<DbMigratorConfiguration>()!;

var connectionString = settings.ConnectionString;

EnsureDatabase.For.PostgresqlDatabase(connectionString);

var engine = DeployChanges.To
    .PostgresqlDatabase(connectionString)
    .WithVariable("DatabaseName", settings.DatabaseName)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
    .LogToConsole()
    .LogScriptOutput()
    .Build();

var result = engine.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
return 0;
