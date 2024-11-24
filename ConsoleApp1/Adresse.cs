using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ConsoleApp1;

[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class Adresse
{
    private int? _etage;

    [XmlElement("etage")]
    public int? _Etage
    {
        set => _etage = (value < 0) ? 0 : value;
        get => _etage;
    }


    private int? _numero;

    [XmlElement("numero")]
    public int? _Numero
    {
        set => _numero = (value < 0) ? 0 : value;
        get => _numero;
    }
    
    private string _rue;
    [XmlElement("rue")]
    public string _Rue
    {
        set => _rue = value;
        get => _rue;
    }
    
    private string _ville;
    [XmlElement("ville")]
    public string _Ville
    {
        set => _ville = value;
        get => _ville;
    }
    
    private string _codePostal;
    [XmlElement("codePostal")]
    public string _CodePostal
    {
        set => _codePostal = (!(Regex.IsMatch(value, @"[1-9][0-9][0-9][0-9][0-9]")) || value.Length != 5)? "38000" : value;

        get => _codePostal;
    }
    public override string ToString() {
        string s = String.Empty;
        if (_etage >= 0)
        {
            s += "etage = " + _etage;
        }

        if (_numero != null)
        {
            s += "\nnumero = " + _numero;
        }

        s += "\nrue = " + _rue;
        s += "\nville = "+_ville;
        s += "\ncode postal = " + _codePostal;
        return s;
    }

}