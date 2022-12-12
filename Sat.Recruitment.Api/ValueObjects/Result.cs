namespace Sat.Recruitment.Api.ValueObjects
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public string? Description { get; set; }
    }
}