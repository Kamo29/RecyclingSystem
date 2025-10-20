namespace Recycling.Logic
{
    internal class UsersLogic
    {
        public string username;
        public string passwordHash;
        public string email;
        public string role;
        public DateTime createdAt;

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public Users() { }

        public Users(string username, string passwordHash, string email, string role, DateTime createdAt)
        {
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
            CreatedAt = createdAt;
        }
    }
}
