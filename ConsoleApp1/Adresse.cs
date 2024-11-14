using System.Reflection.Metadata;
using System.Xml.Serialization;

namespace ConsoleApp1;

[XmlRoot("Adresse", Namespace = "https//www.univ-grenoble-alpes.fr/l3miage")]
[Serializable]
public class Adresse
{
    private string etage;
    [XmlElement("etage")]
    public string _Etage
    {
        get => etage;
    }
    
    private string numero;
    [XmlElement("numero")]
    public string _Numero
    {
        get => numero;
    }
    
    private string rue;
    [XmlElement("rue")]
    public string _Rue
    {
        get => rue;
    }
    
    private string ville;
    [XmlElement("ville")]
    public string _Ville
    {
        get => ville;
    }
    
    private string codePostal;
    [XmlElement("codePostal")]
    public string _CodePostal
    {
        get => codePostal;
    }

}