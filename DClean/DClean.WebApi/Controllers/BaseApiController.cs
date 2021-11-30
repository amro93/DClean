using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace DClean.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {
        //private IMediator _mediator;
        //protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public abstract class BaseVersionedApiController : BaseApiController
    {

    }
}
