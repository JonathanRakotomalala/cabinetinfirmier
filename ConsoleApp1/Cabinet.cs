using System.Xml;
using System.Xml.Serialization;


namespace ConsoleApp1;
[XmlRoot("cabinet",Namespace="http://www.univ-grenoble-alpes.fr/l3miage/medical")][Serializable]
public class Cabinet
{   
    [XmlElement("adresse")]
    public Adresse _Adresse{ get; set; }
    [XmlElement("patients")]
    public Patients _Patients{ get; set; }
    [XmlElement("infirmiers")]
    public Infirmiers _Infirmiers{ get; set; }

    public override string ToString()
    {
        String s=String.Empty;
        s+="-Adresse Cabinet: "+_Adresse.ToString()+"\n-Infirmiers:\n"+_Infirmiers.ToString()+"\n"+"-Patients: \n"+_Patients.ToString()+"\n";
        return s;
    }

    public static void AnalyseGlobale(string filepath)
    {
        var settings = new XmlReaderSettings();
        using (var re = XmlReader.Create(filepath, settings))
        {
            re.MoveToContent();

        while (re.Read())
        {
            switch (re.NodeType)
            {
                case XmlNodeType.Document:
                    Console.WriteLine("ON ENTRE DANS LE DOCUMENT");
                    break; 
                case XmlNodeType.Element: 
                    Console.WriteLine("ON ENTRE DANS L'ELEMENT :"+re.LocalName);
                    if (re.HasAttributes)
                    {
                        int n=re.AttributeCount;
                        Console.WriteLine(" NOMBRE D'ATTRIBUT :"+n);
                        for (int i = 0; i < n; i++)
                        {
                            re.MoveToAttribute(i);
                            Console.WriteLine("     Attribut :"+re.LocalName+":"+re.Value);
                        }
                    }
                    break;
                case XmlNodeType.Text:
                    Console.WriteLine("Text :"+re.Value);
                    break;
                case XmlNodeType.EndElement:
                    Console.WriteLine("ON SORT DE L'ELEMENT "+re.LocalName);
                    break;
            }
        }
        }
    }

    public static void Analysetextparticuliers(string filepath, string nomelement)
    {
        var settings = new XmlReaderSettings();
        using (var re = XmlReader.Create(filepath, settings))
        {
            re.MoveToContent();
            while (re.Read())
            {
                switch (re.NodeType)
                {
                    case XmlNodeType.Element: 
                        re.ReadToFollowing(nomelement);
                        if (re.LocalName == nomelement)
                        {
                            Console.WriteLine(re.LocalName + " :" + re.ReadElementContentAsString());
                        }
                        break;
                }
            }
        }
    }

    public static void Analyseactes(string filepath)
    {
        var settings = new XmlReaderSettings();
        List<string> maliste = new List<string>();
        using (var re = XmlReader.Create(filepath, settings))
        {
            re.MoveToContent();
            while (re.Read())
            {
                switch (re.NodeType)
                {
                    case XmlNodeType.Element:
                        if (re.LocalName == "acte")
                        {
                            re.MoveToFirstAttribute();
                            string acte = re.ReadContentAsString();
                            if (!maliste.Contains(acte))
                            {
                                maliste.Add(acte);
                            }
                        }
                        break;
                }
            }
            Console.WriteLine("actes à réaliser :"+maliste.Count);
        }
    }
    
}