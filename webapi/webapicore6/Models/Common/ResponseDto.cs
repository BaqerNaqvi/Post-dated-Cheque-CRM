using System.Net;

namespace webapicore6.Models.Common
{
    public class ResponseDto<T> where T : class
    {
        public ResponseDto(HttpStatusCode statusCode, string message, T data, PaginationHeader pagination = null, string innerMessage = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Pagination = pagination;
            InnerMessage = innerMessage;
        }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string InnerMessage { get; set; }
        public T Data { get; set; }
        public PaginationHeader Pagination { get; set; }
    }
}
