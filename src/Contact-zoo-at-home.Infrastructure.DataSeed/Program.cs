namespace Contact_zoo_at_home.Infrastructure.DataSeed
{
    /// <summary>
    /// Run manually. Seeds data to the database.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // так, так знаю, але не знаю, як дістати звідки треба(appsettings в WebAPI).
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.webapi;Trusted_Connection=True;MultipleActiveResultSets=true";

            ApplicationSeed applicationSeed = new ApplicationSeed(connectionString);

            applicationSeed.SeedData();
        }
    }
}
