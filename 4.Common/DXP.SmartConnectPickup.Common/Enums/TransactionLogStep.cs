namespace DXP.SmartConnectPickup.Common.Enums
{
    public enum TransactionLogStep
    {
        Unknown = 0,
        CustomExceptionHandlerMiddleware = 1,
        BaseApiControllerExceptionHandling =2,

        // Systerm
        GetCustomer = 10,
        CreateCustomer = 11,
        UpdateCustomer = 12,
        GetOrder = 15,
        CreateOrder = 16,
        UpdateOrder = 17,

        // FlyBuy
        FlyBuyGetCustomer = 20,
        FlyBuyCreateCustomer = 21,
        FlyBuyUpdateCustomer = 22,
    }
}
