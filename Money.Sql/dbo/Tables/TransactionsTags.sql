CREATE TABLE [dbo].[TransactionsTags]
(
	[TransactionId] INT NOT NULL, 
    [TagId] INT NOT NULL, 
    CONSTRAINT [PK_TransactionsTags] PRIMARY KEY ([TagId], [TransactionId])
)
