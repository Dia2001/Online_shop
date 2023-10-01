
using OnlineShop.UnitTest.ControllerTest;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core;
using OnlineShop.Services;
using Xunit;
using FluentAssertions;

namespace OnlineShop.UnitTest.ServiceTest
{
    public class UserServiceTest
    { 
        protected readonly DataContext _context;

        public UserServiceTest()
        { 
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new DataContext(options); 
            _context.Database.EnsureCreated();
        }

        [Fact]
        //naming convention MethodName_ExpectedBehavior_StateUnderTest
        public void GetUser_ListOfUser_UserExistsInRepo()
        {
            // Mock data
            var users = UserMockData.GetSampleUser();
            /// Arrange
            _context.Users.AddRange((IEnumerable<Core.Schemas.Base.UserSchema>)users);
            _context.SaveChanges();

            var userService = new UserService(_context);

            /// Act
            var result = userService.FindAll();

            /// Assert
            result.Should().HaveCount(users.Count);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

    }
}
