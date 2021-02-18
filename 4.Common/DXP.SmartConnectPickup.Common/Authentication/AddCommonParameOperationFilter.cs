using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace DXP.SmartConnectPickup.Common.Authentication
{
    public class AddCommonParameOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Query,
                    Description = "Bearer <token> ",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "Content-Type",
                    In = ParameterLocation.Query,
                    Description = "application/json",
                    Required = false
                });
            }
        }
    }
}
