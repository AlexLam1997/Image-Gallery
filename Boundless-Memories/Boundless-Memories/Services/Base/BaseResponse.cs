namespace Memories.Services.Base
{
    public class BaseResponse
    {
        public ErrorBase Error { get; set; }

        public BaseResponse(ErrorBase error = null)
        {
            Error = error;
        }
    }
}
