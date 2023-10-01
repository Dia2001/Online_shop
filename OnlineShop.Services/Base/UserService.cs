using OnlineShop.Core;
using OnlineShop.Core.Schemas.Base;
using OnlineShop.Services.Base;
using System.Diagnostics;

namespace OnlineShop.Services
{
    public interface IUser : IBaseService<UserSchema>
    {
        UserSchema SetRefreshToken(string refreshToken, int userId);
        UserSchema UpdateLoginTime(int userId);
        List<UserSchema> Get(string name);
        bool CheckPermissionAction(int user, string endPoint);
    }


    public class UserService : BaseService<UserSchema, DataContext>, IUser
    {

        private readonly DataContext context;

        public UserService(DataContext _ctx) : base(_ctx)
        {
            this.context = _ctx;
        }

        public UserSchema UpdateLoginTime(int userId)
        {
            UserSchema user = context.Users.Find(userId);
            user.LastLogin = DateTime.UtcNow;
            return user;
        }

        public UserSchema SetRefreshToken(string refreshToken, int userId)
        {
            UserSchema user = context.Users.Find(userId);
            user.HashRefreshToken = refreshToken;
            return user;
        }

        public List<UserSchema> Get(string name)
        {
            List<UserSchema> users = context.Users.Where(u => u.UserName.Equals(name)).ToList();
            return users;
        }

        public bool CheckPermissionAction(int userId, string endPoint)
        {
            // Stopwatch sw = new Stopwatch(); 
            // sw.Start();

            PermSchema perm = (from p in context.Perms
                               join gp in context.GroupsPerms on p.Id equals gp.PermId
                               join g in context.Groups on gp.GroupId equals g.Id
                               join ug in context.UsersGroups on g.Id equals ug.GroupId
                               where ug.UserId == userId && p.Action == endPoint
                               select p).FirstOrDefault();
            // sw.Stop();

            return perm != null;
        }


    }
}
