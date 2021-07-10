using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels.Responses
{
    public class ActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? ErrorCode { get; set; }
        public ActionResponse(bool success, string message, int? errorCode)
        {
            this.Success = success;
            this.Message = message;
            this.ErrorCode = errorCode;
        }

        public ActionResponse()
        {
            this.Success = true;
        }
    }

    public class ActionResponse<T> : ActionResponse
    {
        public T Result { get; set; }

        public ActionResponse() : base(true, string.Empty, null)
        {
        }

        public ActionResponse(T result) : this()
        {
            this.Result = result;
        }
    }

    public class FailActionResponse : ActionResponse
    {
        public FailActionResponse(string message) : base(false, message, 400)
        {

        }

        public FailActionResponse(string message, int errorCode) : base(false, message, errorCode)
        {

        }
    }
}
