using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebApplication.Models
{
    [Serializable]
    public class Book
    {
        public Book()
        {
        }
        
        [DisplayName("Книга")]
        public string Title { get; set; }
        
        [DisplayName("Категория")]
        public string Category { get; set; } 
        
        
        //TODO: заменить на коллекцию авторов ???
        [DisplayName("Автор")]
        public string Author { get; set; }
        
        [DisplayName("Цена")]
        public double Price { get; set; }
        
        [DisplayName("Год")]
        public string Year { get; set; }
    }
}