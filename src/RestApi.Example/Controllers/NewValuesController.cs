using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestApi.Example.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller to test api
    /// </summary>
    [Route("v{version:apiVersion}/api/values")]
    [ApiVersion("2.1")]
    [ApiController]
    public class NewValuesController : ControllerBase
    {
        private readonly ILogger<NewValuesController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewValuesController"/> class.
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        public NewValuesController(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<NewValuesController>();
        }

        /// <summary>
        ///  GET new api/values
        /// </summary>
        /// <param name="cancellation"><see cref="CancellationToken"/></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            await Task.Delay(0, cancellation).ConfigureAwait(false); // To get rid of the warning

            return Ok(new[] { "value2", "value3" });
        }

        /// <summary>
        /// GET api/values/5
        /// here id is 5
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="cancellation">CancellationToken</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            return await
                ValidateModel(
                    () => Ok("new value"),
                    () => id <= 0,
                    $"{nameof(id)} cannot less or equal to 0")
                    .ConfigureAwait(false);
        }
        
        private async Task<IActionResult> ValidateModel(
            Func<IActionResult> succeed,
            Func<bool> condition,
            string errorMessage)
        {
            try
            {
                if (condition())
                {
                    throw new Exception(errorMessage);
                }
                await Task.Delay(0);//
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Occurs");
                ModelState.AddModelError(string.Empty, ex.Message);

                return BadRequest(ModelState);
            }

            return succeed();
        }
    }
}
