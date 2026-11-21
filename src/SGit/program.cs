// Import important packages
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
using SGit.GitDetector;

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
    SGit.GitDetector.ConfigManager.CheckInstall(settings);
}
else
{

    SGit.Config.ConfigManager.Write(settings);
    SGit.GitDetector.ConfigManager.CheckInstall(settings);

}


