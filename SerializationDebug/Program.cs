using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using BusinessLogic.Model;
using BusinessLogic.Services;

namespace SerializationDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlService = new XmlService();
            var xml = XDocument.Load(@"D:\Files\first.xml");
            var books = xmlService.Read(xml);
            // var serBooks = books.Select(x => new SerializableBook(x)).ToArray();
            // var formatter = new XmlSerializer(typeof(SerializableBook[]), new XmlRootAttribute("bookstore"));
            // using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            // {
            //     formatter.Serialize(fs, serBooks);
            // }
            //xmlService.Write(books);
        }
    }
}