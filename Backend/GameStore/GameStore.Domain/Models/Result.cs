namespace GameStore.Domain.Models
{
    public class Result<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T? Data { get; private set; }

        private Result() { }

        public static Result<T> Ok(T data, string message = "Operation completed successfully.") =>
            new() { Success = true, Data = data, Message = message };

        public static Result<T> Failure(string message) =>
            new() { Success = false, Message = message };
    }
}
