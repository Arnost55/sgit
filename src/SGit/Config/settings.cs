using System;
using YamlDotNet;

namespace SGit.settings;

// settings env
public class Settings
{
    
    public bool FirstTime{ get; set; }= true;
    public bool GitChecked{ get; set; } = false;
    public string GitPath{ get; set; } = "none";
}
