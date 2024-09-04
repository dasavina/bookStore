using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPart.Entities
{
    public class Book : BaseEntity
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public bool InStorage { get; set; }
        public string Cover { get; set; }
        public string Genre { get; set; }
        public int AuthorID { get; set; }

    }
}
