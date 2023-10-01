

using OnlineShop.Core;
using OnlineShop.Services;
using BenchmarkDotNet.Attributes;

namespace OnlineShop.Benchmark.Services
{
    [MemoryDiagnoser]
    public class UserServiceBenchmarks
    {
        private DataContext context;
        private UserService userService;

        public UserServiceBenchmarks()
        {
            context = new DataContext();
            userService = new UserService(context);
        }

        [Benchmark]
        public bool CheckPermissionS1Benchmark()
        {
            int userId = 1;
            string apiEndpoint = "users";
            return userService.CheckPermissionAction(userId, apiEndpoint);
        }

    }
}
