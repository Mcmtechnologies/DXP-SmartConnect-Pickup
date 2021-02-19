using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using Xunit.Abstractions;

namespace DXP.SmartConnectPickup.IntegrationTests
{
    public class GenericIntegrationTest<TStartup> : IClassFixture<CustomWebApplicationFactory<TStartup>> where TStartup: class
    {
        protected readonly CustomWebApplicationFactory<TStartup> _factory;
        protected readonly ITestOutputHelper _output;
        public GenericIntegrationTest(CustomWebApplicationFactory<TStartup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
        }
    }
}
