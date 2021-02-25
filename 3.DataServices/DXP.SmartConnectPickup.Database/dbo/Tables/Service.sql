CREATE TABLE [dbo].[Service] (
    [Id]               NVARCHAR (128) NOT NULL,
    [ServiceName]      NVARCHAR (200) NULL,
    [ServiceShortName] NVARCHAR (10)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

