using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing;
using DXP.SmartConnectPickup.BusinessServices.Services;
using DXP.SmartConnectPickup.Common.ApplicationSettings;
using DXP.SmartConnectPickup.Common.Models;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
                // Arrange create
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                var appSettings = scope.ServiceProvider.GetRequiredService<IOptions<ApplicationSettings>>();
                var merchantSettings = scope.ServiceProvider.GetRequiredService<IOptions<MerchantAccountSettings>>();

                var transactionLogService = scope.ServiceProvider.GetRequiredService<ITransactionLogService>();

                var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                var siteRepository = scope.ServiceProvider.GetRequiredService<ISiteRepository>();
                var service_Service = scope.ServiceProvider.GetRequiredService<IService_Service>();

                var adapterFactory = scope.ServiceProvider.GetRequiredService<IPickupAdapterFactory>();

                var orderApiId = Guid.NewGuid().ToString();

                var model = new OrderModel()
                {
                    OrderApiId = orderApiId,
                    OrderStatus = "New",
                    StoreId = "1",
                    StoreServiceId = "5",
                    PickupType = "Curbside",
                    OrderNumber = "Order number1"
                };

                var orderService = new OrderService(mapper, appSettings, merchantSettings, transactionLogService, orderRepository, siteRepository, adapterFactory, service_Service);

                // Act create
                BaseResponseObject responseCreate = await orderService.CreateOrderAsync(model);

                // Assert create
                Assert.NotNull(responseCreate);
                Assert.NotNull(responseCreate.Data);
                var order = (OrderViewModel)responseCreate.Data;
                _output.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                Assert.True(order.IsSync);

                // Arrange Update
                model.PickupWindow = "2021-03-25T17:57:34.603Z";
                model.OrderStatus = "Ready";
                model.PickupType = "Pickup";

                // Act Update
                BaseResponseObject responseUpdate = await orderService.UpdateOrderAsync(model);

                Assert.NotNull(responseUpdate);
                Assert.NotNull(responseUpdate.Data);
                order = (OrderViewModel)responseUpdate.Data;
                _output.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                Assert.True(order.IsSync);


                // Arrange Change State Order Event
                model.OrderStatus = "Ready";

                // Act Change State Order Event
                BaseResponseObject responseChangeState = await orderService.ChangeStateOrderEvent(model);

                Assert.NotNull(responseChangeState);
                Assert.True(responseChangeState.Status);
            }
        }
    }
}
