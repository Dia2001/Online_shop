
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.UnitTest.ControllerTest
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
				}
			};
			return output;
		}
	}
}
