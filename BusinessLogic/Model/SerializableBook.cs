using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessLogic.Model
{
    [Serializable]
    [XmlType("book")]
    public class SerializableBook
    {
        public SerializableBook()
        {
        }
        
        public SerializableBook(Book book)
        {
            BookTitle = new Title();
            BookTitle.Text = book.Title;
            BookTitle.Language = book.Language;

            Author = book.Author;
            Category = book.Category;
            Price = book.Price;
            Cover = book.Cover;
            Year = book.Year;
        }
        [XmlElement("title")]
        public Title BookTitle { get; set; }

        [XmlAttribute("category")]
        public string Category { get; set; }

        [XmlIgnore]
        public string Author { get; set; }
      
        [XmlElement("author")]
        public List<string> Authors =>
            Author.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("year")]
        public string Year { get; set; }
        

        [XmlAttribute("cover")]
        public string Cover { get; set; }
    }
    [Serializable]
    public class Title
    {
        public Title()
        {
        }

        [XmlAttribute("lang")]
        public string Language { get; set; }
        [XmlText]
        public string Text { get; set; }
        
    }
}
