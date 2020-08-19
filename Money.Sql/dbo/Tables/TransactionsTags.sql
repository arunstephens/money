CREATE TABLE [dbo].[TransactionsTags]
(
	[TransactionId] INT NOT NULL, 
    [TagId] INT NOT NULL, 
    CONSTRAINT [PK_TransactionsTags] PRIMARY KEY ([TagId], [TransactionId]), 
    CONSTRAINT [FK_TransactionsTags_Transactions_TraransactionId] FOREIGN KEY ([TransactionId]) REFERENCES [Transactions]([Id]),
    CONSTRAINT [FK_TransactionsTags_Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tags]([Id]),
)
