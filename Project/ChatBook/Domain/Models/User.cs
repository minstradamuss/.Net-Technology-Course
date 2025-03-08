namespace ChatBook.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; internal set; }
        public string AvatarPath { get; internal set; }
    }

}
