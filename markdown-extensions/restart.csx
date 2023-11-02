#r "nuget: Lestaly, 0.50.0"
#nullable enable
using System.Net.Http;
using System.Threading;
using Lestaly;
using Lestaly.Cx;

var compose_yml = ThisSource.RelativeFile("docker/docker-compose.yml");
var service = new Uri("http://localhost:9902");

ConsoleWig.WriteLineColored(ConsoleColor.Green, "Customize Footnotes");
ConsoleWig.WriteLine($"""
The following must be added to the custom header in the BookStack configuration.
  <script src="https://cdnjs.cloudflare.com/ajax/libs/markdown-it-footnote/3.0.2/markdown-it-footnote.min.js"></script>
  <script src="{service.GetLeftPart(UriPartial.Authority)}/custom/md-it-footnote.js"></script>
""").WriteLine();

ConsoleWig.WriteLineColored(ConsoleColor.Green, "Restart containers.");
await "docker".args("compose", "--file", compose_yml.FullName, "down", "--remove-orphans", "--volumes");
await "docker".args("compose", "--file", compose_yml.FullName, "up", "-d").result().success();
ConsoleWig.WriteLine();

ConsoleWig.WriteLineColored(ConsoleColor.Green, "Wait until it becomes accessible.");
using (var canceller = new CancellationTokenSource(30 * 1000))
using (var client = new HttpClient())
{
    while (!await client.IsSuccessStatusAsync(service, canceller.Token)) await Task.Delay(1000, canceller.Token);
    await CmdShell.ExecAsync(service.AbsoluteUri);
}
