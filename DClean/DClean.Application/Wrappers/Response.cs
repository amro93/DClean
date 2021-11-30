using System;
using System.Collections.Generic;
using System.Text;

namespace DClean.Application.Wrappers
{
    public class Response
    {
        public Response()
        {

        }
        public Response(string message, bool succeded = false)
        {
            Succeeded = succeded;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
    public class Response<T> : Response
    {
        public Response()
        {
        }

        public Response(T data, string message = null) : base(message, true)
        {
            Data = data;
        }

        public Response(string message, bool succeded = false): base (message, succeded)
        {

        }

        public T Data { get; set; }
    }
}
