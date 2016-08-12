using System.ComponentModel.DataAnnotations;

namespace WebAutoLogin.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(40)]
        public string Token { get; set; }

        public bool Locked { get; set; }

        public bool Admin { get; set; }
    }
}