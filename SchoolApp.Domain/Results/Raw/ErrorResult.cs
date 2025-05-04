namespace SchoolApp.Domain.Results.Raw;

public class ErrorResult : ServiceResult
{
    public ErrorResult(string message) : base(false,message) {}
}