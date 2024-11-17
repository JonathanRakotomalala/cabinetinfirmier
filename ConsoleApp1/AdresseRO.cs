using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ConsoleApp1;

[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class AdresseRO
{
    private int? _etage;

    [XmlElement("etage")]
    public int? _EtageRO
    {
        init { _etage = (value <= 0) ? 0 : value; }
        get {return _etage;}
    }


    private int? _numero;

    [XmlElement("numero")]
    public int? _NumeroRO
    {
        init { _numero = (value <= 0) ? 0 : value; }
        get { return _numero; }
    }
    
    private string _rue;
    [XmlElement("rue")]
    public string _RueRO
    {
        init { _rue = value; }
        get { return _rue; }
    }
    
    private string _ville;
    [XmlElement("ville")]
    public string _VilleRO
    {
        init { _ville = value; }
        get { return _ville;}
    }
    
    private string _codePostal;
    [XmlElement("codePostal")]
    public string _CodePostalRO
    {
        init
        { _codePostal = (!(Regex.IsMatch(value, @"[1-9][0-9][0-9][0-9][0-9]")) || value.Length != 5)
                ? "38000" : value;
        }
        get { return _codePostal; }
    }
    public override string ToString() {
        string s = "\n";
        s += "etage = "+_etage;
        s += "\nnumero = " + _numero;
        s += "\nrue = " + _rue;
        s += "\nville = "+_ville;
        s += "\ncode postal = " + _codePostal;
        return s;
    }

}