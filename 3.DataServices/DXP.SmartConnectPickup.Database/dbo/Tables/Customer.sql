CREATE TABLE [dbo].[Customer] (
    [Id]               NVARCHAR (128) NULL,
    [UserId]           NVARCHAR (128) NULL,
    [Provider]         NVARCHAR (256) NULL,
    [ExternalId]       NVARCHAR (128) NULL,
    [ExternalApiToken] NVARCHAR (256) NULL,
    [Name]             NVARCHAR (256) NULL,
    [Phone]            NVARCHAR (128) NULL,
    [CarColor]         NVARCHAR (200) NULL,
    [CarType]          NVARCHAR (200) NULL,
    [LisensePlate]     NVARCHAR (MAX) NULL,
    [TermsOfService]   BIT            NULL,
    [AgeVerification]  BIT            NULL,
    [IsSync]           BIT            NULL,
    [CreatedDate]      DATETIME       NULL,
    [CreatedBy]        NVARCHAR (50)  NULL,
    [ModifiedDate]     DATETIME       NULL,
    [ModifiedBy]       NVARCHAR (50)  NULL
);



