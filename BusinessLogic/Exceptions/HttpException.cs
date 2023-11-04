using System.Net;

namespace BusinessLogic.Exceptions
{

    [Serializable]
	public class HttpException : Exception
	{
        public HttpStatusCode Status { get; set; }
        public HttpException() 
		{
			this.Status = HttpStatusCode.InternalServerError;
        }
		public HttpException(string message, HttpStatusCode status) : base(message) 
		{
			this.Status = status;
		}
		public HttpException(string message, HttpStatusCode status, Exception inner) : base(message, inner) 
		{
            this.Status = status;
        }
		protected HttpException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
