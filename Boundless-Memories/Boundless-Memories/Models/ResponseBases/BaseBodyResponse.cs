namespace Memories.Services.Base
{
    public class BaseBodyResponse<T> : BaseResponse
    {
        public T Payload { get;set; }

        public BaseBodyResponse(T payload) : base()
        {
            Payload = payload;
        }

        public BaseBodyResponse(ErrorBase error) : base(error)
        {

        }
    }
}
