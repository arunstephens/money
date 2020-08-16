CREATE TABLE [dbo].[PayeeAlternateName] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NULL,
    [PayeeId] INT            NULL,
    CONSTRAINT [PK_PayeeAlternateName] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PayeeAlternateName_Payees_PayeeId] FOREIGN KEY ([PayeeId]) REFERENCES [dbo].[Payees] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_PayeeAlternateName_PayeeId]
    ON [dbo].[PayeeAlternateName]([PayeeId] ASC);

