using System.Collections.Generic;
using System.Xml.Serialization;

namespace Celeste_Launcher_Gui.Model;

[XmlRoot("playercolors")]
public class PlayerColors
{
    [XmlElement("player")]
    public List<Player> Players { get; set; } = [];

    [XmlElement("friendorfoeself")]
    public ColorSetting FriendOrFoeSelf { get; set; }

    [XmlElement("friendorfoeally")]
    public ColorSetting FriendOrFoeAlly { get; set; }

    [XmlElement("friendorfoeenemy")]
    public ColorSetting FriendOrFoeEnemy { get; set; }

    [XmlElement("nuggetguardian")]
    public ColorSetting NuggetGuardian { get; set; }
}

public class Player : ColorSetting
{
    [XmlAttribute("num")]
    public int Num { get; set; }
}

public class ColorSetting
{
    [XmlAttribute("color1")]
    public string Color1 { get; set; }

    [XmlAttribute("color2")]
    public string Color2 { get; set; }

    [XmlAttribute("color3")]
    public string Color3 { get; set; }

    [XmlAttribute("minimap")]
    public string Minimap { get; set; }

    [XmlAttribute("userset")]
    public bool IsUserSet { get; set; }
}
