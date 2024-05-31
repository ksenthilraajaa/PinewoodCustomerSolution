using PwCustomer.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = new WebHostBuilder()
                        .UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>()
                        .Build();
        host.Run();
    }
}
