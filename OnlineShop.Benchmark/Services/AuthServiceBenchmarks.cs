
using BenchmarkDotNet.Attributes;
using OnlineShop.Core;
using OnlineShop.Services;

namespace OnlineShop.Benchmark.Services
{
    public class AuthServiceBenchmarks
    {
        private DataContext context;
        private UserService userService;

        public AuthServiceBenchmarks()
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