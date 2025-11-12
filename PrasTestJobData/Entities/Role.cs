using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrasTestJobData.Entities
{
    public class Role
    {
        private int _id;

        public int Id => _id;
        public string Name { get; set; }
        public List<User> Users { get; set; } = new();
    }
}
