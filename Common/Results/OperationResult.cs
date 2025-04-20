namespace authentication_api.Common.Results
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static OperationResult<T> SuccessResult(T data) => new() { Success = true, Data = data };

        public static OperationResult<T> Failure(List<string> errors) => new() { Success = false, Errors = errors };

        public static OperationResult<T> Failure(string error) => new() { Success = false, Errors = new List<string> { error } };
    }
}
