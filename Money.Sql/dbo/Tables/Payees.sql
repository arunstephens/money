CREATE TABLE [dbo].[Payees] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Payees] PRIMARY KEY CLUSTERED ([Id] ASC)
);

