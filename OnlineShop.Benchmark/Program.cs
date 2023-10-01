
using BenchmarkDotNet.Running;
using OnlineShop.Benchmark.Services;

namespace OnlineShop.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<UserServiceBenchmarks>();
            BenchmarkRunner.Run<AuthServiceBenchmarks>(); 
        }  
    }
}
