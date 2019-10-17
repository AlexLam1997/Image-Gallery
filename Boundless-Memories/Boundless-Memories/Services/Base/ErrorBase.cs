namespace Memories.Services.Base
{
    public abstract class ErrorBase
    {
        public int Code { get; }

        public string ErrorMessage { get; set; }

        protected ErrorBase(int code)
        {
            Code = code;
        }

        public bool hasError()
        {
            return Code != 0;
        }
    }
}
