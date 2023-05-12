using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        [Range(18, 81, ErrorMessage = "Invalid age")]
        public int Age { get; set; }
        [MaxLength(20)]
        public string UserName { get; set; }
        [MaxLength(70)]
        public string Password { get; set; }
        [MaxLength(20)]
        public string Email { get; set; }
        [ForeignKey("Region")]
        public string RegionId { get; set; }

        [Required]
        public Region Region { get; set; }
        public List<User> Friends { get; set; }
        public List<Interest> Interests { get; set; }
        private User()
        {
            Friends = new List<User>();
            Interests = new List<Interest>();
        }

        public User(string firstName, string lastName, int age, string userName, string password, string email, Region region)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            UserName = userName;
            Password = password;
            Email = email;
            Region = region;
            Friends = new List<User>();
            Interests = new List<Interest>();
        }
    }
}
