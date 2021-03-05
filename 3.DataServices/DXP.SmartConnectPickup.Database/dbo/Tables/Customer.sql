CREATE TABLE [dbo].[Customer] (
    [Id]               NVARCHAR (128) NOT NULL,
    [UserId]           NVARCHAR (128) NULL,
    [Provider]         NVARCHAR (256) NULL,
    [ExternalId]       NVARCHAR (256) NULL,
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
    [CreatedBy]        DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);





