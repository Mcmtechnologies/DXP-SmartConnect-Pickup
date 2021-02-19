CREATE TABLE [dbo].[TransactionLog] (
    [Id]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [CorrelationId]    UNIQUEIDENTIFIER NULL,
    [TransactionName]  NVARCHAR (50)    NULL,
    [Status]           NVARCHAR (20)    NULL,
    [RequestData]      NVARCHAR (MAX)   NULL,
    [ResponseData]     NVARCHAR (MAX)   NULL,
    [ExceptionMessage] NVARCHAR (MAX)   NULL,
    [Exception]        NVARCHAR (MAX)   NULL,
    [HostName]         VARCHAR (50)     NULL,
    [AdditionalData]   NVARCHAR (MAX)   NULL,
    [CreatedDate]      DATETIME         NULL,
    [CreatedBy]        NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

