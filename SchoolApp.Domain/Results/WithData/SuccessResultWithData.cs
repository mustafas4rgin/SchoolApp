namespace SchoolApp.Domain.Results.WithData;

public class SuccessResultWithData<T> : ResultWithData<T> where T : class
{
    public SuccessResultWithData(string message,T data) : base(true,message,data) {}
} 