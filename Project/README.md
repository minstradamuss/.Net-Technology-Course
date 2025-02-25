# Техническое задание

Проект: "ChatBook"
Разработка мессенджера для участников читального клуба с возможностью общения, обмена отзывами на книги, написания собственных оценок и отзывов на прочитанные книги и добавления друзей.

## Архитектура

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
  // Регистрация и авторизация пользователей
}
public class AuthService : IAuthService {
  // Реализация регистрации и авторизации
}

public interface IBookService {
  // Поиск книг и управление ими
}
public class BookService : IBookService {
  // Реализация поиска и управления книгами
}

public interface IReviewService {
  // Управление отзывами и оценками
}
public class ReviewService : IReviewService {
  // Реализация логики отзывов и оценок
}

public interface IFriendshipService {
  // Управление дружескими связями
}
public class FriendshipService : IFriendshipService {
  // Реализация добавления друзей
}

public interface IChatService {
  // Отправка и получение сообщений
}
public class ChatService : IChatService {
  // Реализация логики чатов
}

public static class NotificationService {
  // Работа с уведомлениями
}
```

##### Таблицы в БД:
1) Users - Id, Nickname, PasswordHash, FirstName, LastName, Avatar, PhoneNumber
2) Books - Id, Title, Author
3) Reviews - Id, UserId, BookId, Content, Rating
4) Friendships - Id, User1Id, User2Id, IsAccepted
5) Messages - Id, SenderId, ReceiverId, Content, Timestamp

#### Паттерны
1) Абстрактная фабрика – AuthService, BookService, ChatService обрабатывают сущности в системе
2) Строитель – Review и UserProfileViewModel наполняются данными пошагово
3) Адаптер – NotificationService взаимодействует с разными методами отправки сообщений
4) Фасад – UserModel объединяет работу с БД и вычисления
5) Стратегия – позволяет изменять реализацию AuthService для различных алгоритмов хеширования
6) Посредник – ChatService управляет взаимодействием между пользователями
