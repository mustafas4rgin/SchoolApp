namespace SchoolApp.Domain.Results.WithData;

public class ErrorResultWithData<T> : ResultWithData<T> where T : class
{
    public ErrorResultWithData(string message) : base(false,message,default!) {}
}