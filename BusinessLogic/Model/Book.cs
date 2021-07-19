using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BusinessLogic.Model
{
    public class Book
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Title { get; set; }

        [DisplayName("Категория")]
        public string Category { get; set; }

        [DisplayName("Автор/ы")]
        public string Author { get; set; }

        
        public string[] Authors =>
            Author.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        [DisplayName("Цена")]
        public string Price { get; set; }

        [DisplayName("Год")]
        public string Year { get; set; }

        [DisplayName("Язык")]
        public string Language { get; set; }

        [DisplayName("Обложка")]
        public string Cover { get; set; }
    }
}