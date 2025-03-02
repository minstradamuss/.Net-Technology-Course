# �������� SOLID

## ������� ������������ ��������������� (SRP)
```c#
// AuthService �������� ������ �� ��������������, � ����������� ������� �������� � ��������� ����� IHashingStrategy
public interface IAuthService {
    void Register(string nickname, string password);
    bool Login(string nickname, string password);
    void Logout(int userId);
    void ChangePassword(int userId, string newPassword);
}
public class AuthService : IAuthService {
    private readonly IHashingStrategy _hashingStrategy;
    
    public AuthService(IHashingStrategy hashingStrategy) {
        _hashingStrategy = hashingStrategy;
    }
    
    public void Register(string nickname, string password) {}
    public bool Login(string nickname, string password) {}
    public void Logout(int userId) {}
    public void ChangePassword(int userId, string newPassword) {}
}
```

## ������� ����������/���������� (OCP)
```c#
// ���������� ����� ��������� ����������� ��� ��������� ������������� ����
public interface IHashingStrategy {
    string HashPassword(string password);
}
public class SHA256HashingStrategy : IHashingStrategy {
    public string HashPassword(string password) {}
}
public class MD5HashingStrategy : IHashingStrategy {
    public string HashPassword(string password) {}
}
```

## ������� ����������� ������� ������ (LSP)
```c#
// ������ ���� ������� ����� �������������� ������ �������� ���� Review
public abstract class Review {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
}
public class TextReview : Review {
    public string Content { get; set; }
}
public class StarReview : Review {
    public int Rating { get; set; }
}
public interface IReviewService {
    void AddReview(Review review);
    void EditReview(int reviewId, string newContent);
    void DeleteReview(int reviewId);
}
public class ReviewService : IReviewService {
    public void AddReview(Review review) {}
    public void EditReview(int reviewId, string newContent) {}
    public void DeleteReview(int reviewId) {}
}
```

## ������� ���������� ����������� (ISP)
```c#
// ���������� ����������� ��� ������ ������������
public interface IBookSearchService {
    List<Book> SearchBooks(string query);
}
public interface IBookManagementService {
    void AddBook(Book book);
    void RemoveBook(int bookId);
}
public class BookService : IBookSearchService, IBookManagementService {
    public List<Book> SearchBooks(string query) { return new List<Book>(); }
    public void AddBook(Book book) {}
    public void RemoveBook(int bookId) {}
}
```

## ������� �������� ������������ (DIP)
```c#
// ������������� ���������� ������ ���������� ����������
public interface IMessageSender {
    void SendMessage(string message);
}
public class EmailSender : IMessageSender {
    public void SendMessage(string message) {}
}
public class SmsSender : IMessageSender {
    public void SendMessage(string message) {}
}
public class NotificationService {
    private readonly IMessageSender _messageSender;
    
    public NotificationService(IMessageSender messageSender) {
        _messageSender = messageSender;
    }
    
    public void Notify(string message) {
        _messageSender.SendMessage(message);
    }
}
```