// Import important packages
using System;
using Settings = SGit.settings.Settings;
using System.IO;
using System.Diagnostics;
using SGit.Config;
using SGit.Git;
using System.Net;


// settings variable
var settings = new Settings
{
    FirstTime = true,
    GitChecked = false,
    GitPath = "none",
};

// init the config file
if (File.Exists(SGit.Config.ConfigManager.ConfigPath))
{
    GitDetector.CheckInstall(settings);
}
else
{

    SGit.Config.ConfigManager.Write(settings);
    GitDetector.CheckInstall(settings);
    
}
SGit.Commands.HistoryCommand.Execute();
