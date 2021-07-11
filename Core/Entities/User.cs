using Core.Entities.Enums;

namespace Core.Entities
{
    public class User
    {
        private User() { }
        public User(string userName, string passwordHash, UserTypes type)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            Type = type;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public UserTypes Type { get; set; }
    }
}
