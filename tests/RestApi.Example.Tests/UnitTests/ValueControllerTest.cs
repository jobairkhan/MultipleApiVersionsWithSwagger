namespace RestApi.Example.Tests.UnitTests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging.Abstractions;
    using Xunit;

    public class ValueControllerTest
    {
        private readonly ValuesController sutController;

        public ValueControllerTest()
        {
            this.sutController = new ValuesController(new NullLoggerFactory());
        }

        [Fact]
        public async Task Get_ShouldReturnValue()
        {
            // Act
            var result = await this.sutController.Get(1, CancellationToken.None);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Post_ShouldValidateValue()
        {
            // Act
            var result = await this.sutController.Post(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
