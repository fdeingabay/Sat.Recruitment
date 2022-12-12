using System;
using Domain.ValueObjects;

namespace Domain.Model.Exceptions
{
    public class InvalidUserTypeException : Exception
    {
        public InvalidUserTypeException(UserType userType)
            : base ($"User type {userType} is not valid")
        {

        }
    }
}