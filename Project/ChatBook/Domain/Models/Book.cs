using System;

namespace ChatBook.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string Status { get; set; }

        public string Review { get; set; }
        public int Rating { get; set; }
        public string CoverImagePath { get; internal set; }

        public override string ToString()
        {
            return $"{Title} - {Author} ({Status})";
        }
    }
}
