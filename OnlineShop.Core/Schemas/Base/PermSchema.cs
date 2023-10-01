using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Core.Schemas.Base
{
    public class PermSchema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string ProfileTypes { get; set; }

    }
}
