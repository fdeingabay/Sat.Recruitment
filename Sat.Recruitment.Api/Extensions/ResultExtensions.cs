using System;
using Sat.Recruitment.Api.ValueObjects;

namespace Sat.Recruitment.Api.Extensions
{
    public static class ResultExtensions
    {
        public static Result ToFailedResult(this Exception ex)
        {
            return new Result { IsSuccess = false, Error = ex.Message };
        }
    }
}