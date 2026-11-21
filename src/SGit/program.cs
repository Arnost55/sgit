// Import important packages
using System;
using Settings = SGit.settings.Settings;
using System.IO;
using System.Diagnostics;
using SGit.Config;
using SGit.Git;
using System.Net;
using SGit.Commands;
using System.Collections;

//write or load the settings
var settings = ConfigManager.LoadOrCreate();

// if 0 args then we show help
if (args.Length == 0)
{
    ShowHelp();
    return;

}

//command what we receive and their arguments
string command = args[0].ToLowerInvariant();

// diffrent command cases
switch (command)
{
    case "history":
    HistoryCommand.Execute();
    break;

    case "install":
        
    case "help":
    case "--help":
    case "-h":
        ShowHelp();
        break;

    default:
    Console.WriteLine($"Uknown command: {command}");
    break;
}

// show help function
static void ShowHelp()
{
    Console.WriteLine(""" 
        SGit - Simple Git for Everyone
        
        Usage:
            sgit   <command>  [arguments]
        Commands:
        history       Show commit history
        help          Show this help message
        """);
}