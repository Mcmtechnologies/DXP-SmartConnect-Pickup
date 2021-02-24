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
    public class OrderServiceTests : GenericIntegrationTest<TestStartup>
    {
        public OrderServiceTests(CustomWebApplicationFactory<TestStartup> factory, ITestOutputHelper output) : base(factory, output)
        {

        }

        [Fact]
        public async Task CreateOrderAsync_ShouldWorkCorrectly()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                // Arrange
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                var appSettings = scope.ServiceProvider.GetRequiredService<IOptions<ApplicationSettings>>();
                var merchantSettings = scope.ServiceProvider.GetRequiredService<IOptions<MerchantAccountSettings>>();

                var transactionLogService = scope.ServiceProvider.GetRequiredService<ITransactionLogService>();

                var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                var siteRepository = scope.ServiceProvider.GetRequiredService<ISiteRepository>(); 

                var adapterFactory = scope.ServiceProvider.GetRequiredService<IPickupAdapterFactory>();

                var orderApiId = Guid.NewGuid().ToString();

                var model = new OrderModel()
                {
                    OrderApiId = orderApiId,
                    OrderStatus = "Created",
                    StoreId = "1",
                    StoreService = "TTO",
                    PickupType = "curbside",
                    OrderNumber ="Order number1"
                };

                var orderService = new OrderService(mapper, appSettings, merchantSettings, transactionLogService, orderRepository,siteRepository, adapterFactory);

                // Act
                BaseResponseObject response = await orderService.CreateOrderAsync(model);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Data);
                var order = (OrderViewModel)response.Data;
                _output.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                Assert.True(order.IsSync);

            }
        }
    }
}
