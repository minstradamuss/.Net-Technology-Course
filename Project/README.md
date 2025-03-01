# Архитектура проекта

## Проект: "ChatBook"
Разработка мессенджера для участников читального клуба с возможностью общения, обмена отзывами на книги, написания собственных оценок и отзывов на прочитанные книги и добавления друзей.

### Функционал и основные сущности

#### Основные функции:
1) Регистрация и авторизация по никнейму и паролю.
2) Профиль пользователя: аватар, имя, фамилия, список книг, статусы чтения (прочитано, читаю, в планах).
3) Оставление отзывов и оценок на прочитанные книги.
4) Поиск книг и пользователей, просмотр их профилей и отзывов.
5) Добавление пользователей в друзья.
6) Чаты для общения, возможность создавать группы.
7) Уведомления (при наличии телефона в профиле).

#### Основные сущности (классы и таблицы БД)

##### Классы:

```c#
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
    public bool Login(string nickname, string password) { return true; }
    public void Logout(int userId) {}
    public void ChangePassword(int userId, string newPassword) {}
}

public interface IBookService {
    Book GetBookById(int bookId);
    void AddBook(Book book);
    void RemoveBook(int bookId);
    List<Book> SearchBooks(string query);
}
public class BookService : IBookService {
    public Book GetBookById(int bookId) { return new Book(); }
    public void AddBook(Book book) {}
    public void RemoveBook(int bookId) {}
    public List<Book> SearchBooks(string query) { }
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

public interface IFriendshipService {
    void SendFriendRequest(int senderId, int receiverId);
    void AcceptFriendRequest(int requestId);
    void RemoveFriend(int userId, int friendId);
}
public class FriendshipService : IFriendshipService {
    public void SendFriendRequest(int senderId, int receiverId) {}
    public void AcceptFriendRequest(int requestId) {}
    public void RemoveFriend(int userId, int friendId) {}
}

public interface IChatService {
    void SendMessage(int senderId, int receiverId, string message);
    List<Message> GetMessages(int userId1, int userId2);
    void DeleteMessage(int messageId);
}
public class ChatService : IChatService {
    public void SendMessage(int senderId, int receiverId, string message) {}
    public List<Message> GetMessages(int userId1, int userId2) { }
    public void DeleteMessage(int messageId) {}
}

public class DatabaseContext {
    private static DatabaseContext _instance;
    private DatabaseContext() { }
    
    public static DatabaseContext Instance {
        get {
            if (_instance == null) {
                _instance = new DatabaseContext();
            }
            return _instance;
        }
    }
}

public interface IObserver {
    void Update(string message);
}
public class NotificationService {
    private List<IObserver> observers = new List<IObserver>();
    
    public void Subscribe(IObserver observer) {
        observers.Add(observer);
    }
    
    public void Notify(string message) {
        foreach (var observer in observers) {
            observer.Update(message);
        }
    }
}
```

##### Таблицы в БД:
1) Users - Id, Nickname, PasswordHash, FirstName, LastName, Avatar, PhoneNumber
2) Books - Id, Title, Author
3) Reviews - Id, UserId, BookId, Content, Rating
4) Friendships - Id, User1Id, User2Id, IsAccepted
5) Messages - Id, SenderId, ReceiverId, Content, Timestamp

![image](https://github.com/user-attachments/assets/2200aa56-d55d-436c-9de8-cc9d5bd97701)

#### Архитектура
![image](https://github.com/user-attachments/assets/4b5c3ad1-9877-4ee3-8682-b2645e4cdfda)

#### Паттерны
1) Factory – используется косвенно через интерфейсы сервисов (IAuthService, IBookService и т. д.), что позволяет внедрять зависимости и подменять реализацию
2) Mediator – ChatService управляет взаимодействием между пользователями, обеспечивая пересылку сообщений
3) Facade – AuthService, BookService, ReviewService, ChatService упрощают доступ к сложной логике, скрывая детали работы с БД
4) Observer – NotificationService управляет подписками и уведомлениями через список observers
5) Singleton – DatabaseContext гарантирует, что будет единственный экземпляр класса для управления базой данных
6) Strategy – в AuthService используется IHashingStrategy, что позволяет подставлять разные алгоритмы хеширования паролей
