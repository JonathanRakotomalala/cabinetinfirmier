namespace ConsoleApp1;
using System.Xml.Serialization;
[XmlRoot("Acte",Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")][Serializable]
public class Acte
{
    [XmlAttribute("id")]
    public string _Id { get; set; }
    
    public override string ToString()
    {
        return _Id;
    }
}