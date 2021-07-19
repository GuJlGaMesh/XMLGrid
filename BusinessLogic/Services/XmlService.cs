using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;
using BusinessLogic.Interfaces;
using BusinessLogic.Model;

namespace BusinessLogic.Services
{
    public class XmlService : IXmlService
    {
        public string Path { get; set; } = Directory.GetCurrentDirectory() + @"\wwwroot\Downloads\";
        public IEnumerable<Book> Read(XDocument xml)
        {
            var books = new List<Book>();
            //var id = 1;
            foreach (XElement book in xml.Element("bookstore").Elements("book"))
            {
                XAttribute category = book.Attribute("category");
                XAttribute cover = book.Attribute("cover");
                XElement year = book.Element("year");
                XElement title = book.Element("title");
                XAttribute language = title.Attribute("lang");
                XElement price = book.Element("price");
                IEnumerable<XElement> author = book.Elements("author");

                if (title != null && category != null && year != null && price != null && author.Count() != 0)
                {
                    var authors = author.Select(x => x.Value);
                    var b = new Book()
                    {
                        //Id = id,
                        Author = string.Join("; ", authors),
                        Category = category.Value,
                        Price = price.Value,
                        Title = title.Value,
                        Year = year.Value,
                        Cover = cover?.Value,
                        Language = language?.Value
                    };
                    books.Add(b);
                   // id++;
                }
            }
            return books;
        }
        
        public void WriteToHTML(IEnumerable<Book> books)
        {
            #region xlst

            string xslt = @"<xsl:transform version=""1.0"" 
   xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
   <xsl:template match=""/"">
   <html>
   <body>
   <h2>Коллекция книг</h2>
   <table border=""1"">
     <tr bgcolor=""#9acd32"">
       <th>Название</th>
       <th>Автор/ы</th>
       <th>Цена</th>
       <th>Год</th>
       <th>Категория</th>
       <th>Обложка</th>
     </tr>
     <xsl:for-each select=""bookstore/book"">
     <tr>
       <td><xsl:value-of select=""title/@lang""/>:<xsl:value-of select=""title""/></td>
       <td> <xsl:for-each select=""author"">
       <xsl:value-of select="".""/>; 
       </xsl:for-each>
       </td>
       <td><xsl:value-of select=""price""/></td>
       <td><xsl:value-of select=""year""/></td>
       <td><xsl:value-of select=""@category""/></td>
       <td><xsl:value-of select=""@cover""/></td>
     </tr>
     </xsl:for-each>
   </table>
   </body>
   </html>
</xsl:template>
</xsl:transform>";

            #endregion
            
            var xml = WriteToXML(books).ToString();
            
            var transform = new XslCompiledTransform();
            using(XmlReader reader = XmlReader.Create(new StringReader(xslt))) {
                transform.Load(reader);
            }
            StringWriter writer = new StringWriter();
            using(XmlReader reader = XmlReader.Create(new StringReader(xml))) {
                transform.Transform(reader, null, writer);
            }

            using (var stream = new FileStream(Path + "report.html", FileMode.OpenOrCreate))
            {
                var buffer = Encoding.Default.GetBytes(writer.ToString());
                stream.Write(buffer, 0, buffer.Length);
            }
        }



// колхоз

        public void WriteToXmlFile(IEnumerable<Book> books)
        {
            RemoveFile("serBooks.xml");
            var serBooks = books.Select(x => new SerializableBook(x)).ToArray();
            
            var formatter = new XmlSerializer(typeof(SerializableBook[]), new XmlRootAttribute("bookstore"));
            using (FileStream fs = new FileStream(Path + "serBooks.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, serBooks);
            }
        }
        public XDocument WriteToXML(IEnumerable<Book> books)
        {
            WriteToXmlFile(books);
            return XDocument.Load(Path + "serBooks.xml");
        }

        private void RemoveFile(string name)
        {

            try
            {
                File.Delete(Path + name);
            }
            catch (Exception e)
            {
            }
        }
    }
}