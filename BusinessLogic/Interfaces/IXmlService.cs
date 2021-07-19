using System.Collections.Generic;
using System.Xml.Linq;
using BusinessLogic.Model;

namespace BusinessLogic.Interfaces
{
    public interface IXmlService
    {
        public IEnumerable<Book> Read(XDocument xml);

        public void WriteToHTML(IEnumerable<Book> books);
        
        public XDocument WriteToXML(IEnumerable<Book> books);
        
        public void WriteToXMLFile(IEnumerable<Book> books);
        
 
    }
}