#r "nuget: Lestaly, 0.57.0"
#r "nuget: Kokuban, 0.2.0"
#nullable enable
using System.Net.Http;
using System.Threading;
using Kokuban;
using Lestaly;
using Lestaly.Cx;

await Paved.RunAsync(config: o => o.NoPause(), action: async () =>
{
    var compose_yml = ThisSource.RelativeFile("docker/docker-compose.yml");
    var service = new Uri("http://localhost:9902");

    Console.WriteLine(Chalk.Green["Customize Footnotes"]);
    Console.WriteLine($"""
    The following must be added to the custom header in the BookStack configuration.
      <script src="https://cdnjs.cloudflare.com/ajax/libs/markdown-it-footnote/3.0.2/markdown-it-footnote.min.js"></script>
      <script src="{service.GetLeftPart(UriPartial.Authority)}/custom/md-it-footnote.js"></script>
    """);
    Console.WriteLine();

    ConsoleWig.WriteLine(Chalk.Green["Restart containers."]);
    await "docker".args("compose", "--file", compose_yml.FullName, "down", "--remove-orphans", "--volumes");
    await "docker".args("compose", "--file", compose_yml.FullName, "up", "-d", "--wait").result().success();
    Console.WriteLine();

    await CmdShell.ExecAsync(service.AbsoluteUri);
});
