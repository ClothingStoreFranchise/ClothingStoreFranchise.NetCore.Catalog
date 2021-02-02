using ClothingStoreFranchise.NetCore.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreFranchise.NetCore.Catalog.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = 500;

            if (exception is EntityAlreadyExistsException) code = 409;
            else if (exception is EntityDoesNotExistException) code = 400;
            else if (exception is InvalidDataException) code = 400;

            return Problem(
                detail: context.Error.StackTrace,
                statusCode: code,
                title: context.Error.Message);
        }
    }
}
