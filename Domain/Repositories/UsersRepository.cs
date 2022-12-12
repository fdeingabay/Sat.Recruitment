using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Domain.Context;
using Domain.Entities;
using Domain.Model.Exceptions;
using Domain.Model.Users;
using Polly;
using Sat.Recruitment.Domain.ValueObjects;

namespace Domain.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        const int nameIndex = 0;
        const int emailIndex = 1;
        const int phoneIndex = 2;
        const int addressIndex = 3;
        const int userTypeIndex = 4;
        const int moneyIndex = 5;

        private readonly IUsersContext usersContext;
        private readonly IAsyncPolicy optimisticConcurrencyExceptionRetryPolicy;

        public UsersRepository(
            IUsersContext usersContext,
            IAsyncPolicy optimisticConcurrencyExceptionRetryPolicy)
        {
            this.usersContext = usersContext ?? throw new ArgumentNullException(nameof(usersContext));
            this.optimisticConcurrencyExceptionRetryPolicy = optimisticConcurrencyExceptionRetryPolicy ?? throw new ArgumentNullException(nameof(optimisticConcurrencyExceptionRetryPolicy));
        }

        public async Task Add(User newUser)
        {
            await this.optimisticConcurrencyExceptionRetryPolicy.ExecuteAsync(async () =>
            {
                using (var fileStream = File.Open(this.usersContext.ConnectionString, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                using (var reader = new StreamReader(fileStream))
                {
                    bool isDuplicated = false;
                    bool isFileEmpty = reader.EndOfStream;

                    while (!reader.EndOfStream && !isDuplicated)
                    {
                        var line = await reader.ReadLineAsync();
                        var lineData = line.Split(',');
                        var user = new UserEntity
                        {
                            Name = lineData[nameIndex],
                            Email = lineData[emailIndex],
                            Phone = lineData[phoneIndex],
                            Address = lineData[addressIndex],
                            UserType = lineData[userTypeIndex],
                            Money = lineData[moneyIndex]
                        };

                        if (user.Email == newUser.Email ||
                            user.Phone == newUser.Phone ||
                            (user.Name == newUser.Name && user.Address == newUser.Address))
                        {
                            isDuplicated = true;
                        }
                    }

                    if (isDuplicated)
                    {
                        throw new DuplicatedUserException(Messages.UserDuplicatedError);
                    }

                    var data = string.Empty;

                    if (isFileEmpty)
                    {
                        data = $"{newUser.Name},{newUser.Email},{newUser.Phone},{newUser.Address},{newUser.UserType},{newUser.Money}";
                    }
                    else
                    {
                        data = $"{Environment.NewLine}{newUser.Name},{newUser.Email},{newUser.Phone},{newUser.Address},{newUser.UserType},{newUser.Money}";
                    }
                    
                    byte[] bytes = Encoding.UTF8.GetBytes(data);

                    await fileStream.WriteAsync(bytes, 0, bytes.Length);
                }
            });
        }
    }
}
