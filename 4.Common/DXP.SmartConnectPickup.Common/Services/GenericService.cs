using DXP.SmartConnectPickup.Common.Models;
using System;

namespace DXP.SmartConnectPickup.Common.Services
{
    public class GenericService
    {
        protected BaseResponseObject ReturnSuccess(object data = null, string message = null, Guid correlationId = default)
        {
            return new BaseResponseObject()
            {
                Status = true,
                CorrelationId = correlationId,
                Data = data,
                Message = message,
            };
        }
    }
}
