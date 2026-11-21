using System;
using System.Diagnostics;
using SGit.Git;

namespace SGit.Git;

static void GitInstall()
{
    if (OperatingSystem.IsWindows())
    {
        var process = new Process();

        process.StartInfo.FileName = "winget.exe";
        process.StartInfo.Arguments = "install Git.Git --accept-package-agreements --accept-source-agreements --force ";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        Console.WriteLine("Starting Git install. The install time depends on your network connection!");
        process.Start();
        process.WaitForExit();
        if (process.ExitCode == 0)
        {
            Console.WriteLine("Git installed succesfully!");
        }
        return null;
    }
    else if (OperatingSystem.IsLinux())
    {
        
        Console.WriteLine("Git install on Linux is currently unsuported because of distros!");
        Console.WriteLine("Install Git manually by your app manager!");
        Console.WriteLine("After that come back and it should work!");
        break;
    }
    else if (OperatingSystem.IsMacOS())
    {
        var process = new Process();

        process.StartInfo.FileName = "brew";
        process.StartInfo.Arguments = "install git";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        Console.WriteLine("Starting Git install. The install time depends on your network connection!");
        process.Start();
        process.WaitForExit();
        if (process.ExitCode == 0)
        {
            Console.WriteLine("Git installed succesfully!");
        }
        return null;
    }
    GitDetector.CheckInstall();
    
}