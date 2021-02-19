CREATE TABLE [dbo].[Setting] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [SettingName]  NVARCHAR (100) NULL,
    [SettingValue] NVARCHAR (MAX) NULL,
    [DisplayOrder] INT            NULL,
    [Description]  NVARCHAR (500) NULL,
    [CreatedBy]    NVARCHAR (50)  NULL,
    [CreatedDate]  DATETIME       NULL,
    [ModifiedBy]   NVARCHAR (50)  NULL,
    [ModifiedDate] DATETIME       NULL,
    [IsDeleted]    BIT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

