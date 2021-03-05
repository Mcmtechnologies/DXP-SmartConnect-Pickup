CREATE TABLE [dbo].[Order] (
    [Id]             NVARCHAR (128) NOT NULL,
    [OrderApiId]     NVARCHAR (128) NULL,
    [Provider]       NVARCHAR (256) NULL,
    [ExternalId]     NVARCHAR (256) NULL,
    [ExternalStatus] NVARCHAR (50)  NULL,
    [RedemptionCode] NVARCHAR (100) NULL,
    [RedemptionUrl]  NVARCHAR (500) NULL,
    [ExternalSiteId] NVARCHAR (256) NULL,
    [PickupType]     NVARCHAR (50)  NULL,
    [PickupWindow]   NVARCHAR (200) NULL,
    [IsSync]         BIT            NULL,
    [DisplayId]      NVARCHAR (200) NULL,
    [StoreService]   NVARCHAR (50)  NULL,
    [StoreId]        NVARCHAR (128) NULL,
    [OrderStatus]    NVARCHAR (50)  NULL,
    [CreatedDate]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (50)  NULL,
    [ModifiedDate]   DATETIME       NULL,
    [ModifiedBy]     NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

