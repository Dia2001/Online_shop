using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Core.Schemas.Base
{
    public class GroupPerm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int GroupId { get; set; }
        public int PermId { get; set; }

        public GroupSchema Group { get; set; }
        public PermSchema Perm { get; set; }
    }
}
