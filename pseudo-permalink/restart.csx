#r "nuget: Lestaly, 0.46.0"
#nullable enable
using System.Net.Http;
using System.Threading;
using Lestaly;
using Lestaly.Cx;

var compose_yml = ThisSource.RelativeFile("docker/docker-compose.yml");
var service = new Uri("http://localhost:9901");

ConsoleWig.WriteLineColored(ConsoleColor.Green, "Pseudo-Permalink by Theme System");
ConsoleWig.WriteLine($"""
  It uses a logical/visual theme system and redirection to achieve pseudo permalinks to each entity.
  Display the Permalink in the detailed information area of the entity.
  This is not a very efficient operation because of the redirection method.
""").WriteLine();

ConsoleWig.WriteLineColored(ConsoleColor.Green, "Restart containers.");
await "docker".args("compose", "--file", compose_yml.FullName, "down", "--remove-orphans", "--volumes");
await "docker".args("compose", "--file", compose_yml.FullName, "up", "-d");
ConsoleWig.WriteLine();

ConsoleWig.WriteLineColored(ConsoleColor.Green, "Wait until it becomes accessible.");
using (var canceller = new CancellationTokenSource(30 * 1000))
using (var client = new HttpClient())
{
    while ((await client.TryGetAsync(service, canceller.Token)) == null)
    {
        await Task.Delay(1000, canceller.Token);
    }
    await CmdShell.ExecAsync(service.AbsoluteUri);
}