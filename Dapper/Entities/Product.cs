
namespace Dapper_Example_Project.Entities
{
    public class Product  : BaseEntity
    {
        public String Name { get; set; }

        public String Properties { get; set; }

        public double Price { get; set; }

        public String Seller { get; set; }

        public String Brand { get; set; }

        public int CategoryId { get; set; }


    }
}
