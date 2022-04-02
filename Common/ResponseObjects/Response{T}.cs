using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ResponseObjects
{
    public class Response<T>:Response,IResponse<T>
    {
        public T Data { get; set; }
        public Response(ResponseType responseType, T data):base(responseType)
        {
            Data = data;
        }
        public Response(ResponseType responseType,T data,List<CustomValidationError> customValidationErrors):base(responseType)
        {
            ValidationErrors = customValidationErrors;
            Data = data;
        }
        public Response(ResponseType responseType, string message):base(responseType,message)
        {

        }
        public List<CustomValidationError> ValidationErrors { get; set; }
    }
}
