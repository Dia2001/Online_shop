using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Core.Schemas.Base
{
    public class UsersGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupId { get; set; }

        public int GroupId { get; set; }
        public int UserId { get; set; }
        public UserSchema User { get; set; }
        public GroupSchema Group { get; set; }
    }
}
