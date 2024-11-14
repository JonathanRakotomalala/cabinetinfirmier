using System.Text;
using System.Xml;
using System.Xml.Resolvers;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ConsoleApp1
{
    public static class XMLUtils
    {
        public static async Task ValidateXmlFileAsync(string schemaNamespace,
            string xsdFilePath, string xmlFilePath)
        {
            var settings = new XmlReaderSettings();
            settings.Schemas.Add(schemaNamespace, xsdFilePath);
            settings.ValidationType = ValidationType.Schema;
            Console.WriteLine("Nombre de schemas utilises dans la validation : " +
                              settings.Schemas.Count);
            settings.ValidationEventHandler += ValidationCallBack;
            var readItems = XmlReader.Create(xmlFilePath, settings);
            while(readItems.Read())
            {
            }
        }

        private static void ValidationCallBack(object? sender, ValidationEventArgs e)
        {
            if (e.Severity.Equals(XmlSeverityType.Warning))
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity.Equals(XmlSeverityType.Error))
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);

            }
        }

        public static void XslTransform(string xmlFilePath, string xsltFilePath, string
            htmlFilePath,string identifiant)
        {
            AppContext.SetSwitch("Switch.System.Xml.Xsl.AllowDefaultResolver", true);
      
            XsltSettings settings = new XsltSettings(true,true);
            
            XmlResolver resolver =new XmlUrlResolver();
            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;
            
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltFilePath,settings, resolver);
            
            XsltArgumentList argList = new XsltArgumentList();
            argList.AddParam("destinedId", "",identifiant);
            
            XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
            XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, Encoding.UTF8);
            xslt.Transform(xpathDoc, argList, htmlWriter,resolver);
            htmlWriter.Close();
            Console.WriteLine($"Transformation complete. Check file at {htmlFilePath}");
        }
        public static void XslTransformpatient(string xmlFilePath, string xsltFilePath, string
            htmlFilePath,string nompatient)
        {
            AppContext.SetSwitch("Switch.System.Xml.Xsl.AllowDefaultResolver", true);
      
            XsltSettings settings = new XsltSettings(true,true);
            
            XmlResolver resolver =new XmlUrlResolver();
            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;
            
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltFilePath,settings, resolver);
            
            XsltArgumentList argList = new XsltArgumentList();
            argList.AddParam("nom", "",nompatient);
            
            XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
            XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, Encoding.UTF8);
            xslt.Transform(xpathDoc, argList, htmlWriter,resolver);
            htmlWriter.Close();
            Console.WriteLine($"Transformation complete. Check file at {htmlFilePath}");

        }
    }
}