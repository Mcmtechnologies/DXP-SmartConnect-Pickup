using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy;
using DXP.SmartConnectPickup.BusinessServices.Services;
using DXP.SmartConnectPickup.Common.ApplicationSettings;
using DXP.SmartConnectPickup.Common.Authentication;
using DXP.SmartConnectPickup.Common.Enums;
using DXP.SmartConnectPickup.Common.WebApi;
using DXP.SmartConnectPickup.DataServices.Context;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using DXP.SmartConnectPickup.DataServices.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using StackifyLib;
using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Security.Principal;

namespace DXP.SmartConnectPickup.IntegrationTests
{
    public class TestStartup
    {
        private const string TERM_OF_SERVICE = "https://example.com/terms";
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        /// The Startup constructor.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public TestStartup(IConfiguration configuration, IHostEnvironment environment)
        {
            // Gets the factory for ILogger instances.
            var nlogLoggerProvider = new NLogLoggerProvider();
            // Creates an ILogger.
            ILogger _logger = nlogLoggerProvider.CreateLogger(typeof(TestStartup).FullName);

            // Gets environment in the web.config file https://weblog.west-wind.com/posts/2020/Jan/14/ASPNET-Core-IIS-InProcess-Hosting-Issues-in-NET-Core-31

            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            _logger.LogInformation($"Environment name: {environmentName}");

            var builder = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true);

            // Sets the new Configuration
            configuration = builder.Build();
            Configuration = configuration;
        }

        private void SetupJWTServices(IServiceCollection services)
        {
            string key = Configuration["Jwt:Key"]; //this should be same which is used while creating token
            CustomAuthenticationHandler.Secret = key;
            services.AddAuthentication("Basic")
                .AddScheme<CustomAuthenticationOptions, CustomAuthenticationHandler>("Basic", op => { });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddControllers();

            services.Configure<ReadmeSettings>(Configuration.GetSection("BuildNote"));
            services.AddDbContext<DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            // Config
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<MerchantAccountSettings>(Configuration.GetSection("MerchantAccountSettings"));
            services.Configure<FaultHandlingSettings>(Configuration.GetSection("FaultHandling"));

            // Service and Repos
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ITransactionLogRepository, TransactionLogRepository>();
            services.AddScoped<ITransactionLogService, TransactionLogService>();
            services.AddScoped<ICachingWorkerService, CachingWorkerService>();
            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<ISiteService, SiteService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            // Use IHttpClientFactory to implement resilient HTTP requests
            // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddSingleton<IWebApiPolicyFactory, WebApiPolicyFactory>();
            services.AddSingleton<IPickupAdapterFactory, PickupAdapterFactory>();
            services.AddHttpClient<IFlyBuyApiAdaptee, FlyBuyApiAdaptee>().AddDefaultFaultHandlingPolicies();
            services.AddScoped<IFlyBuyApiAdapter, FlyBuyApiAdapter>();

            // Dependency injection support for Mapster
            // https://github.com/MapsterMapper/Mapster/wiki/Dependency-Injection
            var config = new TypeAdapterConfig();
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            AddMapperConfigurations(config);

            services.AddLogging(
              builder =>
              {
                  builder.AddFilter("Microsoft", LogLevel.Warning)
                         .AddFilter("System", LogLevel.Warning)
                         .AddFilter("NToastNotify", LogLevel.Warning)
                         .AddConsole();
              });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddMvc().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            SetupJWTServices(services);

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new BadRequestObjectResult(context.ModelState);
                        result.ContentTypes.Add(MediaTypeNames.Application.Json);
                        result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                        return result;
                    };
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                // sepcify our operation filter here.
                c.OperationFilter<AddCommonParameOperationFilter>();

                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "SmartConnectPickup API",
                    Description = "A SmartConnectPickup API ASP.NET Core Web API",
                    TermsOfService = new Uri(TERM_OF_SERVICE),
                    Contact = new OpenApiContact
                    {
                        Name = "Relationshop",
                        Email = string.Empty
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use Relationshop licence"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //Handle Convert Http 204 response to Default Null
            services.AddControllers(options =>
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureStackifyLogging(Configuration); //This is critical!!
            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=HostedPage}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        private void AddMapperConfigurations(TypeAdapterConfig config)
        {
            // Customer
            config.NewConfig<Customer, CreateCustomerRequest>();

            config.NewConfig<Customer, UpdateCustomerRequest>();

            config.NewConfig<Customer, GetCustomerRequest>();

            // Order
            config.NewConfig<Order, CreateOrderRequest>()
                .Map(dest => dest.SiteId, src => src.ExternalSiteId);

            config.NewConfig<Order, UpdateOrderRequest>()
                 .Map(dest => dest.SiteId, src => src.ExternalSiteId);

            // Flybuy config
            FlyBuyAddMapperConfig(config);
        }

        private static void FlyBuyAddMapperConfig(TypeAdapterConfig config)
        {
            // request Customer
            config.NewConfig<CreateCustomerRequest, FlyBuyCustomerRequestData>()
                  .Map(dest => dest.PartnerIdentifier, src => src.UserId);

            config.NewConfig<UpdateCustomerRequest, FlyBuyCustomerRequestData>()
                 .Map(dest => dest.PartnerIdentifier, src => src.UserId);

            // response Customer
            config.NewConfig<FlyBuyCustomerResponseData, BaseCustomerResponse>()
                .Map(dest => dest.UserId, src => src.PartnerIdentifier)
                .Map(dest => dest.ExternalId, src => src.Id)
                .Map(dest => dest.ExternalApiToken, src => src.ApiToken)
                .Include<FlyBuyCustomerResponseData, CreateCustomerResponse>()
                .Include<FlyBuyCustomerResponseData, UpdateCustomerResponse>()
                .Include<FlyBuyCustomerResponseData, GetCustomerResponse>();


            // request Order
            config.NewConfig<CreateOrderRequest, FlyBuyOrderRequestData>()
                  .Map(dest => dest.PartnerIdentifier, src => src.DisplayId)
                  .Map(dest => dest.State, src => ConvertStateForFlyBuyRequest(src.State));

            config.NewConfig<UpdateOrderRequest, FlyBuyOrderRequestData>()
                 .Map(dest => dest.PartnerIdentifier, src => src.DisplayId)
                 .Map(dest => dest.State, src => ConvertStateForFlyBuyRequest(src.State));

            // response Order
            config.NewConfig<FlyBuyOrderResponseData, BaseOrderResponse>()
                .Map(dest => dest.OrderDisplayId, src => src.PartnerIdentifier)
                .Map(dest => dest.ExternalId, src => src.Id)
                .Map(dest => dest.OrderStatus, src => ConvertStateForFlyBuyResponse(src.State))
                .Include<FlyBuyOrderResponseData, CreateOrderResponse>()
                .Include<FlyBuyOrderResponseData, UpdateOrderResponse>()
                .Include<FlyBuyOrderResponseData, GetOrderResponse>();
        }

        private static string ConvertStateForFlyBuyRequest(PickupState state)
        {
            if (state == PickupState.Completed)
            {
                return "completed";
            }
            else if (state == PickupState.Cancelled)
            {
                return "cancelled";
            }
            else if (state == PickupState.Ready)
            {
                return "ready";
            }
            else if (state == PickupState.Delayed)
            {
                return "delayed";
            }

            return "created";
        }

        private static PickupState ConvertStateForFlyBuyResponse(string state)
        {
            if (state == "completed")
            {
                return PickupState.Completed;
            }
            else if (state == "cancelled")
            {
                return PickupState.Cancelled;
            }
            else if (state == "ready")
            {
                return PickupState.Ready;
            }
            else if (state == "delayed")
            {
                return PickupState.Delayed;
            }

            return PickupState.Created;
        }
    }
}
