using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace DXP.SmartConnectPickup.IntegrationTests.BusinessService
{
    public class TransactionLogServiceTests : GenericIntegrationTest<TestStartup>
    {
        public TransactionLogServiceTests(CustomWebApplicationFactory<TestStartup> factory, ITestOutputHelper output) : base(factory, output)
        {

        }
    }
}
