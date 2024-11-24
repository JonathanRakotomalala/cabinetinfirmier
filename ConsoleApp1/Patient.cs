using System.Text.RegularExpressions;
using System.Xml.Serialization;
namespace ConsoleApp1;

[XmlRoot("patient", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class Patient
{
    [XmlElement("nom")]
    public string _Nom { get; set; }
    [XmlElement("prénom")]
    public string _Prenom { get; set; }
    [XmlElement("sexe")]
    public string _Sexe { get; set; }
    [XmlElement("naissance")]
    public string _Naissance { get; set; }
    [XmlElement("numero")]
    public string _Numero { get; set; }
    [XmlElement("adresse")]
    public Adresse _Adresse { get; set; }
    [XmlElement("visite")]
    public Visite _Visite { get; set; }

    public override string ToString()
    {
        String s = String.Empty;
        s = s+"Nom et Prénom :"+_Nom+" "+_Prenom;
        s = s + "\nSexe :" + _Sexe;
        s = s + "\nNaissance :" + _Naissance;
        s = s + "\nNumero :" + _Numero;
        s = s+"\nAdresse :" + _Adresse;
        s= s + "\nVisite :" + _Visite;
        return s;
    }
}