using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Clicks { get; set; }
        public string ImageUrl { get; set; }
    }
}
