using System.Xml.Serialization;

[XmlRoot("infirmier", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class Infirmier
{
    [XmlElement("id")]
    public string _Id { get; set; }

    [XmlElement("nom")]
    public string _Nom { get; set; }

    [XmlElement("prénom")]
    public string _Prenom { get; set; }

    [XmlElement("photo")]
    public string _Photo { get; set; }

    public override string ToString()
    {
        return $"Id: {_Id}, Nom: {_Nom}, Prénom: {_Prenom}, Photo: {_Photo}";
    }
}