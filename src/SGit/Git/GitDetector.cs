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
using System.Runtime.CompilerServices;
namespace SGit.Git;



public static class GitDetector
{
    public const string ConfigPath = "config.yaml";
    // find git function
    public static string? FindGit()
    {
        if (OperatingSystem.IsWindows())
        {
            var process = new Process();

            process.StartInfo.FileName = "where.exe";
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
        } else if (OperatingSystem.IsMacOS() || OperatingSystem.IsLinux)
        {
            process.StartInfo.FileName = "which";
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
        else
        {
            Console.WriteLine($"Uknown OS. Exitting...");
        }
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
