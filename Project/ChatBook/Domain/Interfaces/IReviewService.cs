using ChatBook.Entities;
using System.Collections.Generic;

namespace ChatBook.Domain.Interfaces
{
    public interface IReviewService
    {
        void AddReview(Review review);
        List<Review> GetReviews(int bookId);
    }
}
