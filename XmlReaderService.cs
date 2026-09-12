using System.Xml;

namespace CodeReviewCsharpTest;

public class XmlReaderService
{
    // XXE vulnerability: XmlDocument without entity resolution disabled
    public void ParseXml(string untrustedXml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(untrustedXml);
    }
}
