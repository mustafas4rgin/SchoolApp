namespace SchoolApp.Domain.Results.Raw;

public class SuccessResult : ServiceResult
{
    public SuccessResult(string message) : base (true, message) {}
}