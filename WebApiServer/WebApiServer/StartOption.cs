using CommandLine;

namespace Server
{
    public class StartOption
    {
        [Option('c', "console", HelpText = "Start server as console mode.")]
        public bool AsConsole { get; set; }

        [Option('e', "env", HelpText = "Set the environment of appsetting.json.")]
        public string Environment { get; set; }
    }
}