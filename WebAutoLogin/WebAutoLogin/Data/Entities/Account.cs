namespace WebAutoLogin.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public bool IsLocked { get; set; }

        public bool IsAdmin { get; set; }
    }
}