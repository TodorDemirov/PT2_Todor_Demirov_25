using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Interest
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Region> Regions { get; set; }
        private Interest()
        {
            Users = new List<User>();
            Regions = new List<Region>();
        }

        public Interest(string name)
        {
            Name = name;
            Users = new List<User>();
            Regions = new List<Region>();
        }
    }
}
