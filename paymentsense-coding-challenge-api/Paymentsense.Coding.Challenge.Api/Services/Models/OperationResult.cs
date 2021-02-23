namespace Paymentsense.Coding.Challenge.Api.Services.Models
{
    public class OperationResult<T> : IOperationResult<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }
        
        public T Data { get; set; }
    }

    public class EmptyOperationResult : IOperationResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
