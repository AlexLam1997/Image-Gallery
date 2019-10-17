using Memories.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Memories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
		/// <summary>
		/// Returns a Http response with the BaseResponse as content and status code corresponding to error 
		/// code or 200 if no error
		/// </summary>
		/// <param name="response"></param>
		/// <returns></returns>
        public IActionResult ProcessResponse(BaseResponse response)
        {
            var statusCode = MapHttpStatusCode(response?.Error);
            return StatusCode((int)statusCode, response);
        }

        private HttpStatusCode MapHttpStatusCode(ErrorBase error)
        {
            if (error?.Code == null || error.Code == 0)
            {
                return HttpStatusCode.OK;
            }

            if (error.Code >= 4000 && error.Code <= 4999)
            {
                return HttpStatusCode.BadRequest;
            }

            if (error.Code >= 5000 && error.Code <= 5999)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
