using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RestApi.Example
{
    /// <summary>
    /// Main Program
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// The Main program
        /// </summary>
        /// <param name="args">string[]; Command line arg</param>
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args)
                .Build()
                .Run();

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
