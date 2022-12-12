using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Sat.Recruitment.Test.Domain.Repositories
{
    public class UsersFixture : IDisposable
    {
        private readonly string connectionString;

        public UsersFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            this.connectionString = Directory.GetCurrentDirectory() + configuration["UsersConnectionString"];

            using (File.Create(connectionString))
            {
            }
        }

        public string ConnectionString => this.connectionString;

        public void Dispose()
        {
            File.Delete(connectionString);
        }
    }
}