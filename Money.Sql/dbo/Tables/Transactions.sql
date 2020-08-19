CREATE TABLE [dbo].[Transactions] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [ExternalId]              NVARCHAR (MAX)  NULL,
    [AccountId]               INT             NOT NULL,
    [TransactionDate]         DATETIME2 (7)   NOT NULL,
    [ProcessedDate]           DATETIME2 (7)   NULL,
    [PayeeName]               NVARCHAR (MAX)  NULL,
    [Amount]                  DECIMAL (18, 2) NOT NULL,
    [Particulars]             NVARCHAR (MAX)  NULL,
    [Code]                    NVARCHAR (MAX)  NULL,
    [Reference]               NVARCHAR (MAX)  NULL,
    [BankTransactionType]     NVARCHAR (MAX)  NULL,
    [OtherPartyAccountNumber] NVARCHAR (MAX)  NULL,
    [Serial]                  NVARCHAR (MAX)  NULL,
    [BankSerial]              NVARCHAR (MAX)  NULL,
    [BankTransactionCode]     NVARCHAR (MAX)  NULL,
    [BankBatchNumber]         NVARCHAR (MAX)  NULL,
    [OriginatingBankBranch]   NVARCHAR (MAX)  NULL,
    [CardSuffix]              NVARCHAR (MAX)  NULL,
    [PayeeId]                 INT             NULL,
    [CategoryId]              INT             NULL,
    [DataSource]              NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transactions_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Transactions_Payees_PayeeId] FOREIGN KEY ([PayeeId]) REFERENCES [dbo].[Payees] ([Id]),
    CONSTRAINT [FK_Transactions_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Transactions_AccountId]
    ON [dbo].[Transactions]([AccountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Transactions_PayeeId]
    ON [dbo].[Transactions]([PayeeId] ASC);

