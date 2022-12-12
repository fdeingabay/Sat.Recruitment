using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model.Exceptions;
using Domain.ValueObjects;

namespace Domain.Model.Users.Factory
{
    public class UserFactoryResolver : IUserFactoryResolver
    {
        private readonly IEnumerable<IUserFactory> availableFactories;

        public UserFactoryResolver(IEnumerable<IUserFactory> availableFactories)
        {
            this.availableFactories = availableFactories ?? throw new ArgumentNullException(nameof(availableFactories));
        }

        public IUserFactory GetFactory(UserType userType)
        { 
            var factory = this.availableFactories.SingleOrDefault(x => x.UserType == userType);

            if (factory == null)
            {
                throw new InvalidUserTypeException(userType);
            }

            return factory;
        }
    }
}