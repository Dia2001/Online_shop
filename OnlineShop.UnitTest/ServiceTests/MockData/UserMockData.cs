
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.UnitTest.ServiceTest.MockData
{
    public class UserMockData
    {
        public static List<UserSchema> GetSampleUser()
        {
            List<UserSchema> output = new List<UserSchema>
            {
                new UserSchema
                {
                    UserName = "Jhon",
                    Email = "jhon@gmail.com",
                    GroupIds = "[1]",
                    Password = "123456",
                }
            };
            return output;
        }
    }
}
