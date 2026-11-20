// Import important packages
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Settings = SGit.settings;
using System.IO;


// init the config file
const string CONF = "./config.yaml";
if (File.Exists(CONF))
{
    
}
else
{
    
    var settings = new SGit.settings.Settings
    {
        FirstTime = true,
        GitChecked = false,
        GitPath = "none",
    };
    // yaml file writer
    var serializer = new SerializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
    var yaml = serializer.Serialize(settings);
    Console.WriteLine("Waiting for file creation!");
    File.WriteAllText(CONF, yaml);


}


// check whether Git is installed
public class CheckInstall
{
    
}
