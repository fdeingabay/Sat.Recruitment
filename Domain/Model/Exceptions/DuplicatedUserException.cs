using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Exceptions
{
    public class DuplicatedUserException : Exception
    {
        public DuplicatedUserException(string message) : base(message)
        {
        }
    }
}
