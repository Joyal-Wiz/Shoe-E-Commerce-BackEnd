namespace ECommerce.Application.Responses
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; }
        public bool Success { get; }
        public string Message { get; }
        public T? Data { get; }
        public IEnumerable<string>? Errors { get; }

        private ApiResponse(int statusCode, bool success, string message, T? data, IEnumerable<string>? errors)
        {
            StatusCode = statusCode;
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public static ApiResponse<T> SuccessResponse(string message, T data, int statusCode = 200)
        {
            return new ApiResponse<T>(statusCode, true, message, data, null);
        }

        public static ApiResponse<T> FailureResponse(string message, int statusCode = 400, IEnumerable<string>? errors = null)
        {
            return new ApiResponse<T>(statusCode, false, message, default, errors);
        }
    }
}