namespace ECommerce.Application.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        private ApiResponse(bool success, string message, T? data, IEnumerable<string>? errors)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public static ApiResponse<T> SuccessResponse(string message, T data)
        {
            return new ApiResponse<T>(true, message, data, null);
        }

        public static ApiResponse<T> FailureResponse(string message, IEnumerable<string>? errors = null)
        {
            return new ApiResponse<T>(false, message, default, errors);
        }
    }
}
