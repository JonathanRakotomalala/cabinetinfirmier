namespace ConsoleApp1;
using System.Xml.Serialization;
[XmlRoot("patients", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Patients
{
    [XmlElement("patient")]
    public List<Patient> _Patients { get; set; }
    
    public override string ToString() {
        return string.Join("\n", _Patients.Select(i => i.ToString()));
    }
}