using System.Xml.Serialization;

namespace ConsoleApp1;
[XmlRoot("infirmiers",Namespace="http://www.univ-grenoble-alpes.fr/l3miage/medical") ][Serializable]

public class Infirmiers
{
    [XmlElement("infirmier")]
    public List<InfirmierRO> _Infirmiers{get;set;}
    
    public override string ToString() {
        return string.Join("\n", _Infirmiers.Select(i => i.ToString()));
    }
}