using System.Reflection.Metadata;
using System.Xml.Serialization;

namespace ConsoleApp1;
[XmlRoot("infirmier", Namespace = "http:/www.univ-grenoble-alpes.fr/l3miage/medical")][Serializable]
public class InfirmierRO
{
    private string _id;
    [XmlElement("id")]
    public string _IdRO
    {
        init => _id = (!(value.All(Char.IsDigit)) || value.Length != 3) ? "999" : value;
        get => _id;
    }

    private string _nom;
    [XmlElement("nom")]
    public string _NomRO
    {
        init => _nom = value; 
        get => _nom; 
    }
    
    private string _prenom;
    [XmlElement("prénom")]
    public string _PrenomRO
    {
        init => _prenom = value; 
        get => _prenom; 
    }
    
    private string _photo;
    [XmlElement("photo")]
    public string _PhotoRO
    {
        init => _photo = value; 
        get => _photo; 
    }
    
    public override string ToString()
    {
        return _nom;
    }
}