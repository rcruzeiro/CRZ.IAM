using System;
using System.Threading;
using System.Threading.Tasks;
using CRZ.Framework.Web.Messages;
using CRZ.IAM.API.Messages.Account.Request;
using CRZ.IAM.API.Messages.Account.Response;
using CRZ.IAM.Platform.Application.Account;
using Microsoft.AspNetCore.Mvc;

namespace CRZ.IAM.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        readonly IAccountAppService _accountAppService;

        public AuthController([FromServices]IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService ?? throw new ArgumentNullException(nameof(accountAppService));
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="request">Create request parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Created status code.</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesErrorResponseType(typeof(DefaultErrorMessage))]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                await _accountAppService.CreateUser(request.Command, cancellationToken);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                var errorMessage = new DefaultErrorMessage(500, ex);

                return StatusCode(500, errorMessage);
            }
        }
        /// <summary>
        /// Login a user.
        /// </summary>
        /// <param name="request">Login request parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User's login token.</returns>
        [HttpPut]
        [ProducesDefaultResponseType(typeof(LoginUserResponse))]
        [ProducesErrorResponseType(typeof(DefaultErrorMessage))]
        public async Task<IActionResult> LoginUser([FromBody]LoginUserRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _accountAppService.LoginUser(request.Command, cancellationToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorMessage = new DefaultErrorMessage(500, ex);

                return StatusCode(500, errorMessage);
            }
        }
        /// <summary>
        /// Logout a user.
        /// </summary>
        /// <param name="request">Logout request parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content status code.</returns>
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesErrorResponseType(typeof(DefaultErrorMessage))]
        public async Task<IActionResult> LogoutUser([FromBody]LogouUserRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                await _accountAppService.LogoutUser(request.Command, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                var errorMessage = new DefaultErrorMessage(500, ex);

                return StatusCode(500, errorMessage);
            }
        }
    }
}