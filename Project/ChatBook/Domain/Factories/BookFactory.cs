using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBook.Domain.Factories
{
    using ChatBook.Entities;
    using System;

    public interface IBookFactory
    {
        Book Create(string title, string author, string genre, string status, int rating, string review, byte[] coverImage, int userId);
    }

    public class BookFactory : IBookFactory
    {
        public Book Create(string title, string author, string genre, string status, int rating, string review, byte[] coverImage, int userId)
        {
            return new Book
            {
                Title = title,
                Author = author,
                Genre = genre,
                Status = status,
                Rating = rating,
                Review = review,
                CoverImage = coverImage,
                UserId = userId
            };
        }
    }
}
