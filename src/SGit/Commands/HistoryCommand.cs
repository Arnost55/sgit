using SGit.Config;
using SGit.Git;


namespace SGit.Commands;


public static class HistoryCommand
{
    public static void Execute()
    {
        var result = GitRunner.Run(
            "log",
            "--oneline",
            "-10"
        );
        if (result.Success)
        Console.WriteLine(result.Output);
        else
        Console.WriteLine(result.Error);

    }
}
