using ChatBook.Domain.Models;

namespace ChatBook
{
    public static class AppSession
    {
        public static User LoggedUser { get; private set; }

        public static void SetLoggedUser(User user)
        {
            if (LoggedUser == null)
            {
                LoggedUser = user;
            }
        }
    }
}
