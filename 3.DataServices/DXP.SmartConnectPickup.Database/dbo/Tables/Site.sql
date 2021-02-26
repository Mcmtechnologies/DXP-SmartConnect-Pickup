CREATE TABLE [dbo].[Site] (
    [Id]           NVARCHAR (128) NOT NULL,
    [Provider]     NVARCHAR (256) NULL,
    [ExternalId]   NVARCHAR (256) NULL,
    [StoreId]      NVARCHAR (128) NULL,
    [StoreCode]    NVARCHAR (50)  NULL,
    [JsonObject]   NVARCHAR (MAX) NULL,
    [CreatedDate]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (50)  NULL,
    [ModifiedDate] DATETIME       NULL,
    [ModifiedBy]   NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

