namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, object result = null, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Result = result;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Fulfiled, was the Request",
                201 => "Created, was the Resource",
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate.  Hate leads to career change.",
                _ => null
            };
        }
    }
}