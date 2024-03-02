#r "nuget: Lestaly, 0.57.0"
#r "nuget: Kokuban, 0.2.0"
#nullable enable
using System.Threading;
using Kokuban;
using Lestaly;
using Lestaly.Cx;

await Paved.RunAsync(async () =>
{
    var composeFile = ThisSource.RelativeFile("./docker/docker-compose.yml");
    var bindFile = ThisSource.RelativeFile("./docker/volume-bind.yml");
    var service = new Uri("http://localhost:9901");

    Console.WriteLine(Chalk.Green["Pseudo-Permalink by Theme System"]);
    Console.WriteLine($"""
      It uses a logical/visual theme system and redirection to achieve pseudo permalinks to each entity.
      Display the Permalink in the detailed information area of the entity.
      This is not a very efficient operation because of the redirection method.
    """);
    Console.WriteLine();

    ConsoleWig.WriteLine(Chalk.Green["Restart containers."]);
    await "docker".args("compose", "--file", composeFile.FullName, "down", "--remove-orphans", "--volumes");
    await "docker".args("compose", "--file", composeFile.FullName, "--file", bindFile.FullName, "up", "-d", "--wait").result().success();
    Console.WriteLine();

    await CmdShell.ExecAsync(service.AbsoluteUri);
});
