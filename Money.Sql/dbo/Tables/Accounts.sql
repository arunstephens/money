﻿CREATE TABLE [dbo].[Accounts] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX)  NULL,
    [Number]         NVARCHAR (MAX)  NULL,
    [OpeningBalance] DECIMAL (18, 2) DEFAULT ((0.0)) NOT NULL,
    [PocketSmithId] NVARCHAR(MAX) NULL, 
    [AccountTypeId] INT NULL, 
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Accounts_AccountTypes_AccountTypeId] FOREIGN KEY ([AccountTypeId]) REFERENCES [AccountTypes]([Id])
);

