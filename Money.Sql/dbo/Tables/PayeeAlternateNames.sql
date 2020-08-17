CREATE TABLE [dbo].[PayeeAlternateNames] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NULL,
    [PayeeId] INT            NULL,
    CONSTRAINT [PK_PayeeAlternateNames] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PayeeAlternateNames_Payees_PayeeId] FOREIGN KEY ([PayeeId]) REFERENCES [dbo].[Payees] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_PayeeAlternateNames_PayeeId]
    ON [dbo].[PayeeAlternateNames]([PayeeId] ASC);

