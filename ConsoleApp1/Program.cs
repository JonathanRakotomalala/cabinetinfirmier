using System.Security.AccessControl;
using System.Xml;

namespace ConsoleApp1
{
    public class DOM2Xpath
    {
        private XmlDocument doc;

        public DOM2Xpath(String filename)
        {
            doc = new XmlDocument();
            doc.Load(filename);
        }

        public XmlNodeList getXPath(String nsPrefix, String nsURI, String expression)
        {
            XmlNode root = doc.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace(nsPrefix, nsURI);
            return root.SelectNodes(expression, nsmgr);
        }

        public XmlNode avoirRacine(String filename)
        {
            XmlDocument doc;
            doc = new XmlDocument();
            doc.Load(filename);
            XmlNode root = doc.DocumentElement;
            return root;
        }
    }



    internal class Program
    {
        public static int count(string elementName)
        {
            
            var c = 0;
            if (elementName == "patient" || elementName == "infirmier")
            {
                var expression = "//cab:cabinet//" + "cab:" + elementName + "s" + "//cab:" + elementName +
                                 "/cab:prénom";
                
                DOM2Xpath cabin = new DOM2Xpath("C:\\Users\\jon4t\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\data\\xml\\cabinet.xml");
                XmlNodeList noeuds = cabin.getXPath("cab",
                    "http://www.univ-grenoble-alpes.fr/l3miage/medical",
                    expression);
             

                foreach (XmlNode noeud in noeuds)
                {
                    c = c + 1;
                }
            }
            else
            {
                c = -1;

            }
            return c;
        }
            public static int count2(string elementName,XmlDocument doc)
            {
                var c = 0;
                if (elementName == "patient" || elementName == "infirmier")
                {
                    var expression = "//cab:cabinet//" + "cab:" + elementName + "s" + "//cab:" + elementName +
                                     "/cab:prénom";
                    
                    XmlNodeList noeuds = doc.GetElementsByTagName(elementName+"s");
                    
                    foreach (XmlNode noeud in noeuds.Item(0))
                    {
                        c = c + 1;
                    }
                }
                else
                {
                    c = -1;

                }

                return c;
            }
            

        public static bool hasAdresse(string elementName)
        {
            DOM2Xpath cabinetDOM =
                new DOM2Xpath("C:\\Users\\jon4t\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\data\\xml\\cabinet.xml");
            if (elementName == "cabinet")
            {
                XmlNodeList cabinet = cabinetDOM.getXPath("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical",
                    "//cab:cabinet/cab:adresse");

                foreach (XmlNode cabinetNode in cabinet)
                {
                    if (!adresseComplete(cabinetNode, elementName)) return false;
                }
            }
            else if (elementName == "patient")
            {
                XmlNodeList patients = cabinetDOM.getXPath(
                    "cab",
                    "http://www.univ-grenoble-alpes.fr/l3miage/medical",
                    "//cab:cabinet//cab:patients//cab:patient/cab:adresse");

                foreach (XmlNode patientNode in patients)
                {
                    if (!adresseComplete(patientNode, elementName)) return false;

                }
            }
            else
            {
                return false;

            }

            return true;
        }

        public static bool adresseComplete(XmlNode adresseNode, string elementName)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlNode rueNode = adresseNode.SelectSingleNode("cab:rue", nsmgr);
            XmlNode villeNode = adresseNode.SelectSingleNode("cab:ville", nsmgr);
            XmlNode codePostalNode = adresseNode.SelectSingleNode("cab:codePostal", nsmgr);
            return (rueNode != null && villeNode != null && codePostalNode != null);

        }

        public static bool numerosocialvalide(XmlNode infosNode, string numSoc, XmlNamespaceManager nsmgr)
        {
            XmlNode sexeNode = infosNode.SelectSingleNode("cab:sexe", nsmgr);
            XmlNode naissanceNode = infosNode.SelectSingleNode("cab:naissance", nsmgr);

            string datenaissance = naissanceNode!.InnerText;
            char s;

            if (sexeNode.InnerText == "M")
            {
                s = '1';
            }
            else
            {
                s = '2';
            }

            return numSoc[0] == s && numSoc.Substring(1, 6) == (datenaissance.Replace("-", "")).Substring(2, 6);
            
        }

        public static bool allnumerosocialvalide(string xmlFilepath)
        {
            DOM2Xpath cabinetDOM = new DOM2Xpath(xmlFilepath);
            XmlNodeList informationsNode = cabinetDOM.getXPath(
                "cab",
                "http://www.univ-grenoble-alpes.fr/l3miage/medical",
                "//cab:cabinet//cab:patients//cab:patient");
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical");

            foreach (XmlNode patient in informationsNode)
            {
                string numSoc = patient.SelectSingleNode("cab:numero", nsmgr).InnerText;
                if (!numerosocialvalide(patient, numSoc, nsmgr)) return false;
            }

            return true;
        }

        public static void ajouteinfirmier(string nom,string prenom)
        {
            XmlDocument doc;
            doc = new XmlDocument();
            doc.Load("C:\\Users\\jon4t\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\data\\xml\\cabinet.xml");
            
            XmlNode root=doc.DocumentElement;
            XmlNamespaceManager nmsgr = new XmlNamespaceManager(new NameTable());
            nmsgr.AddNamespace("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            
            Console.WriteLine("Nombre d'infirmiers avant :"+count2("infirmier",doc));
            
            XmlElement infirmier = doc.CreateElement("infirmier", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            
            XmlElement id = doc.CreateElement("id", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText idText = doc.CreateTextNode("005");
            id.AppendChild(idText);
            
            XmlElement nominfirmier = doc.CreateElement("nom","http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText nomText = doc.CreateTextNode(nom);
            nominfirmier.AppendChild(nomText);
            
            XmlElement prenominfirmier = doc.CreateElement("prénom", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText prenomText = doc.CreateTextNode(prenom);
            prenominfirmier.AppendChild(prenomText);
            
            XmlElement photo = doc.CreateElement("photo", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText photoText = doc.CreateTextNode(prenom+".png");
            photo.AppendChild(photoText);
            
            infirmier.AppendChild(id);
            infirmier.AppendChild(nominfirmier);
            infirmier.AppendChild(prenominfirmier);
            infirmier.AppendChild(photo);
            
            XmlNodeList infirmiers =  doc.GetElementsByTagName("infirmiers");
            
            infirmiers.Item(0).AppendChild(infirmier);
            Console.WriteLine("Nombre d'infirmiers après rajout d'un infirmier :"+count2("infirmier",doc));
        }

        ajoutepatient(string nom, string prenom,string sexe, string naissancedate,string adresse)
        {
            XmlDocument doc;
            doc = new XmlDocument();
            doc.Load("C:\\Users\\jon4t\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\data\\xml\\cabinet.xml");
            
            XmlNode root=doc.DocumentElement;
            XmlNamespaceManager nmsgr = new XmlNamespaceManager(new NameTable());
            nmsgr.AddNamespace("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            
            Console.WriteLine("Nombre de patients avant :"+count2("patient",doc));
            
            XmlElement patient = doc.CreateElement("patient", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            

            XmlElement nompatient = doc.CreateElement("nom","http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText nomText = doc.CreateTextNode(nom);
            nompatient.AppendChild(nomText);
            
            XmlElement prenompatient = doc.CreateElement("prénom", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText prenomText = doc.CreateTextNode(prenom);
            prenompatient.AppendChild(prenomText);
            
            XmlElement sexepatient = doc.CreateElement("sexe", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText sText = doc.CreateTextNode(sexe);
            sexepatient.AppendChild(sText);

            
            XmlElement naissance = doc.CreateElement("naissance", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText nText = doc.CreateTextNode(naissancedate);
            naissance.AppendChild(nText);
            
                        
            XmlElement numsoc = doc.CreateElement("numero", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText numText = doc.CreateTextNode(numerosoc);
            numsoc.AppendChild(numText);
            
            XmlElement adressepatient = doc.CreateElement("adresse", "http://www.univ-grenoble-alpes.fr/l3miage/medical");

            XmlElement ruepatient = doc.CreateElement("rue", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText rText = doc.CreateTextNode();
            ruet.AppendChild(rText);
            
            XmlElement villepatient = doc.CreateElement("ville", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText vText = doc.CreateTextNode();
            villepatient.AppendChild(vText);

            XmlElement cppatient = doc.CreateElement("codePostal", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlText cText = doc.CreateTextNode();
            cppatient.AppendChild(cText);
            
            adressepatient.AppendChild(ruepatient);
            adressepatient.AppendChild(villepatient);
            adessepatient.appendChild(cppatient);

            
            
            patient.AppendChild(nompatient);
            patient.AppendChild(prenompatient);
            patient.AppendChild(sexepatient);
            patient.AppendChild(naissance);
            patient.AppendChild(numsoc);
            patient.AppendChild(adressepatient);
            
            XmlNodeList patients =  doc.GetElementsByTagName("patients");
            
            patients.Item(0).AppendChild(patient);
            Console.WriteLine("Nombre d'infirmiers après rajout de patients :"+count2("patient",doc));
        }

        public void ajoutevisite(string nom,string datev,string actev,string intervenantv)
        {
            XmlDocument doc= new XmlDocument();
            doc.Load();
            XmlNodeList noeuds = doc.GetElementsByTagName("patient");
            XmlNode patient;
            foreach (XmlNode noeud in noeuds)
            {
                if (noeud.FirstChild.InnerText == nom)
                {
                    patient = noeud;
                }
            }
            XmlElement visite = doc.CreateElement("visite","http://www.univ-grenoble-alpes.fr/l3miage/medical");
            
            XmlElement date = doc.CreateElement("date","http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlNodeText dText = doc.CreateTextNode(datev);
            date.AppendChild(dText);
            
            XmlElement intervenant = doc.CreateElement("intervenant","http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlNodeText iText = doc.CreateTextNode(intervenantv);
            intervenant.AppendChild(iText);
            
            XmlElement acte = doc.CreateElement("acte","http://www.univ-grenoble-alpes.fr/l3miage/medical");
            XmlAttribute acteid = doc.CreateAttribute("id");
            XmlNodeText acteText = doc.CreateTextNode(actev);
            acteid.AppendChild(acteText);
            acte.AppendChild(acteid);

            visite.AppendChild(date);
            visite.AppendChild(acte);
            visite.AppendChild(intervenant);

            patient.AppendChild(visite);
        }

        private static void Main()
        {
            Console.WriteLine("------------------------------------");
            XMLUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/cabinet.xsd",
                "./data/xml/cabinet.xml ");
            Console.WriteLine("------------------------------------");
            XMLUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/actes", "./data/xsd/actes.xsd",
                "./data/xml/actes.xml ");
            Console.WriteLine("------------------------------------");
            XMLUtils.XslTransform("./data/xml/cabinet.xml", "./data/xslt/page1.xslt",
                "C:\\Users\\jon4t\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\data\\html\\page1.html", "003");
            Console.WriteLine("------------------------------------");
            XMLUtils.XslTransformpatient("./data/xml/cabinet.xml", "./data/xslt/page2.xslt",
                "C:\\Users\\jon4t\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\data\\xml\\nompatient.xml", "Kapoëtla");
            Console.WriteLine("------------------------------------");
            Cabinet.AnalyseGlobale("./data/xml/cabinet.xml");
            Console.WriteLine("------------------------------------");
            Cabinet.Analysetextparticuliers("./data/xml/cabinet.xml", "prénom");
            Console.WriteLine("------------------------------------");
            Cabinet.Analyseactes("./data/xml/cabinet.xml");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Nombre d'infirmiers " + count("infirmier"));
            Console.WriteLine("Nombre de patients " + count("patient"));
            Console.WriteLine("Adresse cabinet complète? :" + hasAdresse("cabinet"));
            Console.WriteLine("Adresse patients complètes? :" + hasAdresse("patient"));
            Console.WriteLine("Tous les numero de secu valide? " + allnumerosocialvalide("C:\\Users\\jon4t\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\data\\xml\\cabinet.xml"));
            ajouteinfirmier("Némard","Jean");
        }
    }
}