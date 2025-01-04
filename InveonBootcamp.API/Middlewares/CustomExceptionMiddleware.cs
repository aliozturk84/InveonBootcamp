using InveonBootcamp.Business.Abstract;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;

namespace InveonBootcamp.API.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            // Response.Body'yi tamponlayacak bir memory stream oluşturuluyor
            var originalBodyStream = context.Response.Body;
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream; // Response.Body'yi memoryStream'e yönlendiriyoruz

                try
                {
                    await _next(context); // Request işleniyor
                    watch.Stop();
                    var settings = new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    };

                    // Burada Response Body'yi okuyoruz ve logluyoruz
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = new StreamReader(memoryStream).ReadToEnd();

                    // JSON'dan sadece 'message' ve 'errorMessage' alanlarını almak
                    var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);
                    if (jsonResponse != null)
                    {
                        string message = "No message"; // Varsayılan değer
                        if (jsonResponse.ContainsKey("message"))
                        {
                            var messageValue = jsonResponse["message"];
                            message = messageValue != null ? messageValue.ToString() : "No message"; // null kontrolü ekledik
                        }

                        string errorMessage = "No error message"; // Varsayılan değer
                        if (jsonResponse.ContainsKey("errorMessage"))
                        {
                            var errorMessageValue = jsonResponse["errorMessage"];
                            errorMessage = errorMessageValue != null ? JsonConvert.SerializeObject(errorMessageValue, settings) : "No error message"; // null kontrolü ekledik
                        }

                        // Mesajları logluyoruz
                        _loggerService.Write($"Response Message: {message}, ErrorMessage: {errorMessage}");
                    }

                    // Stream'i tekrar başa sarıyoruz ve yanıtı yazdırıyoruz
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(originalBodyStream);
                }
                catch (Exception ex)
                {
                    watch.Stop();
                    await HandleException(context, ex, watch);
                }
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Request.EnableBuffering();

            string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + ": Error Message - " + ex.Message + " (in " + watch.Elapsed.TotalMilliseconds + " ms.)";
            _loggerService.Write(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, settings);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }

    }
}
