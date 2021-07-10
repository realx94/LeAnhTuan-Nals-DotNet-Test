using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [ApiController]
    [Authorize]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult CreateSuccessResult<T>(T result)
        {
            return Ok(new ActionResponse<T>(result));
        }

        protected IActionResult CreateSuccess()
        {
            return Ok(new ActionResponse());
        }

        protected IActionResult CreateFailResult(string error)
        {
            return this.BadRequest(new FailActionResponse(error));
        }

        protected IActionResult CreateFailResult(string error, int errorCode)
        {
            return this.BadRequest(new FailActionResponse(error, errorCode));
        }

    }
}
