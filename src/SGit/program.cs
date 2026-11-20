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

// settings variable
var settings = new Settings
{
    FirstTime = true,
    GitChecked = false,
    GitPath = "none",
};

// init the config file
const string CONF = "./config.yaml";
if (File.Exists(CONF))
{
    CheckInstall(settings);
}
else
{

    yamlwrite(settings);
    CheckInstall(settings);

}

// yaml file writer
static void yamlwrite(Settings settings)
{
    
    var serializer = new SerializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
    var yaml = serializer.Serialize(settings);
    File.WriteAllText(CONF, yaml);

}

// yaml file reader
static SGit.settings.Settings yamlread()
{
    var yamlcontent = File.ReadAllText(CONF);
    var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
    var config = deserializer.Deserialize<SGit.settings.Settings>(yamlcontent);
    return config;
}

// find git function
static string? FindGit()
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
static void CheckInstall(Settings? settings)
{   
    var configread = yamlread();
    string? gitPath = FindGit();
    if (gitPath != null)
    {
        settings.GitChecked = true;
        settings.GitPath = gitPath;
        Console.WriteLine($"Git found: {gitPath}");
    } else
    {
        settings.GitChecked = false;
        settings.GitPath = "none";
        Console.WriteLine("Git not installed or not in path!");
    }
    yamlwrite(settings);
}

