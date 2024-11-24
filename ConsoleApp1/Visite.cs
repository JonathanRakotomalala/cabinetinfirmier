using System.Xml.Serialization;
namespace ConsoleApp1;
[XmlRoot("visite",Namespace="http://www.univ-grenoble-alpes.fr/l3miage/medical")][Serializable]
public class Visite
{
    [XmlElement("date")]
    public string _Date { get; set; }
    [XmlElement("intervenant")]
    public string _Intervenant { set; get; }
    [XmlElement("acte")]
    public Acte _Acte { set; get; }
    
    public override string ToString()
    {
        return "\ndate de visite \n"+_Date + "\nintervenant " + _Intervenant + "\nacte " + _Acte+"\n";
    }
    
}