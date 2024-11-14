using System.Xml;

namespace ConsoleApp1;

public class Cabinet
{
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