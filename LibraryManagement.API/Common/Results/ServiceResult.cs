using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Common.Results
{
    public class ServiceResult<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Success(T data, string message = "İşlem Başarılı")
        {
            return new ServiceResult<T>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = message,
                Data = data
            };
        }

        public static ServiceResult<T> Fail(string title, string detail, int statusCode = StatusCodes.Status400BadRequest)
        {
            return new ServiceResult<T>
            {
                StatusCode = statusCode,
                Message = title,
                Data = default
            };
        }
    }

}