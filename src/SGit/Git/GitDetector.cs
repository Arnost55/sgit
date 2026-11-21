using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Settings = SGit.settings.Settings;
using System.IO;
using System.Diagnostics;
using SGit.Config;
namespace SGit.Git;



public static class GitDetector
{
    public const string ConfigPath = "config.yaml";
    // find git function
    public static string? FindGit()
    {
        var process = new Process();

        process.StartInfo.FileName = OperatingSystem.IsWindows() ? "where.exe" : "which";
        process.StartInfo.Arguments = "git";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        if (process.ExitCode == 0)
        {
            return output.Split('\n')[0].Trim();
        }
        return null;

    }
    // check whether Git is installed
    public static void CheckInstall(Settings? settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        string? gitPath = FindGit();
        if (gitPath != null)
        {
            settings.GitChecked = true;
            settings.GitPath = gitPath;
            Console.WriteLine($"Git found: {gitPath}");
        }
        else
        {
            settings.GitChecked = false;
            settings.GitPath = "none";
            Console.WriteLine("Git not installed or not in path!");
        }
        SGit.Config.ConfigManager.Write(settings);
    }
}
