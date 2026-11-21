using System;
using System.Diagnostics;
using System.IO;
using SGit.Config;

namespace SGit.Git;

public static class GitRunner
{
    public static GitResult Run(params string[] arguments)
    {
        // Read Git path from YAML
        var settings = ConfigManager.Read();

        // Configure Git process
        var startInfo = new ProcessStartInfo
        {
            FileName = settings.GitPath,
            WorkingDirectory = Directory.GetCurrentDirectory(),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // Add Git arguments
        foreach (var argument in arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        // Run Git command
        using var process = new Process
        {
            StartInfo = startInfo
        };

        process.Start();

        // Read output and errors
        var outputTask = process.StandardOutput.ReadToEndAsync();
        var errorTask = process.StandardError.ReadToEndAsync();

        process.WaitForExit();

        return new GitResult
        {
            ExitCode = process.ExitCode,
            Output = outputTask.GetAwaiter().GetResult(),
            Error = errorTask.GetAwaiter().GetResult()
        };
    }
}