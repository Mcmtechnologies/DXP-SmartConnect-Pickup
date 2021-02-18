namespace DXP.SmartConnectPickup.Common.Enums
{
    public enum PaymentMethod
    {
        Unknown = 0,
        CreateCustomerProfile = 1,
        GetCustomerProfile = 2,
        DeleteCustomerProfile = 3,
        GetToken = 20,
        CreateAccount = 30,
        GetAccount = 31,
        GetAccounts = 32,
        DeleteAccount = 33,
        Sale = 40,
        SaleStatus = 41,
        Authorize = 50,
        Capture = 60,
        Void = 70,
        VoidOfSale = 71,
        VoidOfAuthorize = 72,
        Refund = 80,
        RefundOfSale = 81,
        RefundOfCapture = 82,
        Cancel = 90,
    }
}
