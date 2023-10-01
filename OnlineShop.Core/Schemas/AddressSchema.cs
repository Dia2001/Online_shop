using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Core.Schemas
{
    public class AddressSchema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId {get;set;}
        public ICollection<CustomerSchema> Customers {get;set;}
        public string Address1 {get;set;}
        public string Address2 { get;set; }
        public string City {get;set;}
        public string Country {get;set;}
        public int Zip {get;set;}
    }
}