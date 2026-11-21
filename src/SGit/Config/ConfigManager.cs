using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Settings = SGit.settings.Settings;
namespace SGit.Config;



public static class ConfigManager
{
    public const string ConfigPath = "config.yaml";
    public static Settings Read()
    {
        var yaml = File.ReadAllText((ConfigPath));
        var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
        return deserializer.Deserialize<Settings>(yaml);

    }
    public static void Write(Settings settings)
    {
        var serializer = new SerializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
        var yaml = serializer.Serialize(settings);
        File.WriteAllText(ConfigPath, serializer.Serialize(settings));

    }
}

