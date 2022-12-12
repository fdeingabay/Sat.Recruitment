namespace Domain.Context
{
    public class UsersContext : IUsersContext
    {
        private readonly string connectionString;

        public UsersContext(string connectionString) => this.connectionString = connectionString;

        public string ConnectionString => this.connectionString;
    }
}