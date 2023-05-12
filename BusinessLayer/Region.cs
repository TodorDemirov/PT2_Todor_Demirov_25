using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Region
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Interest> Interests { get; set; }
        private Region()
        {
            Users = new List<User>();
            Interests = new List<Interest>();
        }
        public Region(string name)
        {
            Name = name;
            Users = new List<User>();
            Interests = new List<Interest>();
        }
    }
}
