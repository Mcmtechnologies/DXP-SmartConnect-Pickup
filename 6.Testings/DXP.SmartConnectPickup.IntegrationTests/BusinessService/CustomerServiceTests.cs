using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy;
using DXP.SmartConnectPickup.BusinessServices.Services;
using DXP.SmartConnectPickup.Common.ApplicationSettings;
using DXP.SmartConnectPickup.Common.Enums;
using DXP.SmartConnectPickup.Common.Models;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DXP.SmartConnectPickup.IntegrationTests.BusinessService
{
    public class CustomerServiceTests : GenericIntegrationTest<TestStartup>
    {
        public CustomerServiceTests(CustomWebApplicationFactory<TestStartup> factory, ITestOutputHelper output) : base(factory, output)
        {

        }

        [Fact]
        public async Task CreateCustomerAsync_ShouldWorkCorrectly()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                // Arrange
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                var appSettings = scope.ServiceProvider.GetRequiredService<IOptions<ApplicationSettings>>();
                var merchantSettings = scope.ServiceProvider.GetRequiredService<IOptions<MerchantAccountSettings>>();
                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                var transactionLogService = new Mock<ITransactionLogService>();

                var adapterFactory = new Mock<IPickupAdapterFactory>();

                var adapter = new Mock<IFlyBuyApiAdapter>();

                var merchantAccount = new MerchantAccount();

                merchantAccount.SetMerchantAccountType(merchantSettings.Value.PickupProviderDefault);

                adapterFactory.Setup(x => x.BuildPickupAdapter(It.Is<MerchantAccount>(x => x.MerchantAccountType == merchantAccount.MerchantAccountType)))
                    .Returns(adapter.Object);

                var userId = Guid.NewGuid().ToString();

                var model = new CustomerModel()
                {
                    AgeVerification = true,
                    TermsOfService = true,
                    Name = "Create customer Integration test service",
                    UserId = userId,
                    Phone = "212-200-0555",
                };

                var flyBuyErrorResponse = "IntegrationTest: Cannot create customer in Flybuy";
                object flyBuyErrorObj = new { flyBuyErrorResponse };
                adapter.Setup(x => x.CreateCustomerAsync(It.IsAny<CreateCustomerRequest>(), It.IsAny<string>()))
                    .ReturnsAsync(new CreateCustomerResponse() { Errors = flyBuyErrorObj });

                adapter.Setup(x => x.CreateCustomerAsync(It.IsAny<CreateCustomerRequest>(), It.IsAny<Guid>()))
                    .ReturnsAsync(new CreateCustomerResponse() { Errors = flyBuyErrorResponse });

                var customerService = new CustomerService(mapper, appSettings, merchantSettings, transactionLogService.Object, customerRepository, adapterFactory.Object);

                // Act
                BaseResponseObject response = await customerService.CreateCustomerAsync(model);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Data);
                var customer = (CustomerViewModel)response.Data;
                Assert.False(customer.IsSync);

                transactionLogService.Verify(x => x
                    .AddTransactionLogAsync(TransactionLogStep.CreateCustomer, TransactionLogStatus.Error, It.IsAny<string>(), JsonConvert.SerializeObject(flyBuyErrorResponse), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);

                _output.WriteLine(JsonConvert.SerializeObject(customer,Formatting.Indented));
            }
        }
    }
}
