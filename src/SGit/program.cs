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
using System.Diagnostics;



// init the config file
const string CONF = "./config.yaml";
if (File.Exists(CONF))
{
    CheckInstall();
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
    CheckInstall();

}


// check whether Git is installed
static void CheckInstall()
{   
    var yamlcontent = File.ReadAllText(CONF);
    var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
    var config = deserializer.Deserialize<SGit.settings.Settings>(yamlcontent);
    Console.WriteLine(config.FirstTime);
}
