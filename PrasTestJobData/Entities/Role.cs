using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PrasTestJobData.Entities
{
    public class Role
    {
        private int _id;

        public int Id {
            get { return _id; }
            init 
            {
                if (value > 0)
                    _id = value;
            }
        }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new();
    }
}
