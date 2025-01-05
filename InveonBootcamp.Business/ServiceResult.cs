using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InveonBootcamp.Business
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessage { get; set; }
        public string? Message { get; set; }
        [JsonIgnore] public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore] public bool IsFail => !IsSuccess;
        [JsonIgnore] public HttpStatusCode Status { get; set; }
        [JsonIgnore] public string? UrlAsCreated { get; set; }


        public static ServiceResult<T> Success(T data,string message, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult<T>()
            {
                Data = data,
                Message = message,
                Status = status
            };
        }


        public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
        {
            return new ServiceResult<T>()
            {
                Data = data,
                Status = HttpStatusCode.Created,
                UrlAsCreated = urlAsCreated
            };
        }


        public static ServiceResult<T> Fail(List<string> errorMessage, string message, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = errorMessage,
                Message = message,
                Status = status
            };
        }


        public static ServiceResult<T> Fail(string errorMessage,string message, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = [errorMessage],
                Message = message,
                Status = status
            };
        }
    }


    public class ServiceResult
    {
        public List<string>? ErrorMessage { get; set; }
        public string? Message { get; set; } 
        public string? IyzicoResult { get; set; }
        public int? ProcessId { get; set; }
        [JsonIgnore] public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore] public bool IsFail => !IsSuccess;
        [JsonIgnore] public HttpStatusCode Status { get; set; }


        public static ServiceResult Success(string message, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult()
            {
                Message = message,  
                Status = status
            };
        }
        public static ServiceResult Success(string message, string iyzicoResult, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult()
            {
                Message = message,
                Status = status,
                IyzicoResult = iyzicoResult
            };
        }
        public static ServiceResult Success(string message, int processId, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult()
            {
                Message = message,
                Status = status,
                ProcessId = processId
            };
        }


        public static ServiceResult Fail(List<string> errorMessage,
            HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = errorMessage,
                Status = status
            };
        }


        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = [errorMessage],
                Status = status
            };
        }
    }
}
