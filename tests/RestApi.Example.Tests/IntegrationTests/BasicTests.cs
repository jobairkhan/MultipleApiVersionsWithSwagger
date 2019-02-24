namespace RestApi.Example.Tests.IntegrationTests
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public abstract class BasicTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        protected BasicTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "TEST");
            this.HttpClientInstance = factory.CreateClient();
        }

        protected HttpClient HttpClientInstance { get; private set; }
    }
}
