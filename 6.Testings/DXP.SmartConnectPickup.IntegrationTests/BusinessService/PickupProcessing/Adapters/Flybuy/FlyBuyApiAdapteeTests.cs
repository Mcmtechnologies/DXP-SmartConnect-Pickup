using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DXP.SmartConnectPickup.IntegrationTests.BusinessService.PickupProcessing.Adapters.Flybuy
{
    public class FlyBuyApiAdapteeTests : GenericIntegrationTest<TestStartup>
    {
        public FlyBuyApiAdapteeTests(CustomWebApplicationFactory<TestStartup> factory, ITestOutputHelper output) : base(factory, output)
        {

        }

        [Fact]
        public async Task CURD_Customer_ShouldWorkCorrectly()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                // arrange create
                var adaptee = scope.ServiceProvider.GetRequiredService<IFlyBuyApiAdaptee>();

                var customerId = Guid.NewGuid().ToString();

                var requestCreate = new FlyBuyCreateCustomerRequest()
                {
                    Data = new FlyBuyCustomerRequestData()
                    {
                        Name = "Create customer IntegrationTest",
                        AgeVerification = true,
                        TermsOfService = true,
                        PartnerIdentifier = customerId,
                        CarColor = "Car Red",
                        CarType = "Car Type",
                        Phone = "212-200-0555"
                    }
                };
                var correlationId = Guid.NewGuid().ToString();

                // act create
                FlyBuyCreateCustomerResponse responseCreate = await adaptee.CreateCustomerAsync(requestCreate, correlationId);

                // assert create
                Assert.Null(responseCreate.RequestError);
                Assert.NotNull(responseCreate.Data);
                Assert.NotNull(responseCreate.Data.Id);
                Assert.Equal(responseCreate.Data.PartnerIdentifier, customerId);

                // arrange update
                var resquestUpdate = new FlyBuyUpdateCustomerRequest()
                {
                    Id = responseCreate.Data.Id,
                    Data = new FlyBuyCustomerRequestData()
                    {
                        Name = "Update customer IntegrationTest",
                        AgeVerification = true,
                        TermsOfService = true,
                        PartnerIdentifier = customerId,
                        CarColor = "Car Red",
                        CarType = "Car Type",
                        Phone = "212-200-0555"
                    }
                };

                // act update
                FlyBuyUpdateCustomerResponse responseUpdate = await adaptee.UpdateCustomerAsync(resquestUpdate, correlationId);

                Assert.Null(responseUpdate.RequestError);
                Assert.NotNull(responseUpdate.Data);
                Assert.NotNull(responseUpdate.Data.Id);
                Assert.Equal(responseUpdate.Data.PartnerIdentifier, customerId);

                // arrange Get
                var requestGet = new FlyBuyGetCustomerRequest()
                {
                    Id = responseUpdate.Data.Id
                };

                FlyBuyGetCustomerResponse responseGet = await adaptee.GetCustomerAsync(requestGet, correlationId);
                Assert.Null(responseGet.RequestError);
                Assert.NotNull(responseGet.Data);
                Assert.NotNull(responseGet.Data.Id);
                Assert.Equal(responseGet.Data.PartnerIdentifier, customerId);

                _output.WriteLine(JsonConvert.SerializeObject(responseGet.Data,Formatting.Indented));
            }
        }
    }
}
