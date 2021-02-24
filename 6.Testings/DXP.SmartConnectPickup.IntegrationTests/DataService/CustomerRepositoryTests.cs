using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using Xunit;
using Xunit.Abstractions;

namespace DXP.SmartConnectPickup.IntegrationTests.DataService
{
    public class CustomerRepositoryTests : GenericIntegrationTest<TestStartup>
    {

        public CustomerRepositoryTests(CustomWebApplicationFactory<TestStartup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]

        public void CreateAndUpdateEntity_ShouldWorkCorrect()
        {
            using (var scrope = _factory.Services.CreateScope())
            {
                // arrage
                string name = "Customer integration test repository created";
                var customer = new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    IsSync = false,
                    Name = name
                };

                ICustomerRepository customerRepository = scrope.ServiceProvider.GetRequiredService<ICustomerRepository>();

                // act create
                customerRepository.Add(customer);
                customerRepository.Commit();

                // assert create
                Assert.Equal(customer.Name, name);

                // act update 
                customer.Name = "Customer integration test repository update";
                customerRepository.Update(customer);
                customerRepository.Commit();

                // assert create
                Assert.NotEqual(customer.Name, name);

                _output.WriteLine(JsonConvert.SerializeObject(customer, Formatting.Indented));
            }
        }
    }
}
