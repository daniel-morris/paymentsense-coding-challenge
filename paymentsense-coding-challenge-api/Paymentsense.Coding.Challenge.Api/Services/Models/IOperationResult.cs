namespace Paymentsense.Coding.Challenge.Api.Services.Models
{
    public interface IOperationResult<T> : IOperationResult
    {
        T Data { get; set; }
    }

    public interface IOperationResult
    {
        bool Success { get; set; }

        string Message { get; set; }
    }
}
