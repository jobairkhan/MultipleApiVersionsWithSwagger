using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestApi.Example.Controllers
{
    /// <summary>
    /// Controller to test api
    /// </summary>
    [Route("v{version:apiVersion}/api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        public ValuesController(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<ValuesController>();
        }

        /// <summary>
        ///  GET api/values
        /// </summary>
        /// <param name="cancellation"><see cref="CancellationToken"/></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            await Task.Delay(0, cancellation).ConfigureAwait(false); // To get rid of the warning

            return Ok(new[] { "value1", "value2" });
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
                    () => Ok("value"),
                    () => id <= 0,
                    $"{nameof(id)} cannot less or equal to 0")
                    .ConfigureAwait(false);
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value">[FromBody] string </param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            return await
                ValidateModel(
                    Ok,
                    () => string.IsNullOrWhiteSpace(value),
                    $"{nameof(value)} cannot be null or empty")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// PUT api/values/5
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="value">string, [FromBody]</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            return await
                ValidateModel(
                    () => Ok("value"),
                    () => id <= 0 && string.IsNullOrWhiteSpace(value),
                    $"Invalid {nameof(id)} or {nameof(value)}")
                    .ConfigureAwait(false);
        }

        /// <summary>
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id">int</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await
               ValidateModel(
                   () => Ok("value"),
                   () => id <= 0,
                   $"Invalid {nameof(id)}")
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
