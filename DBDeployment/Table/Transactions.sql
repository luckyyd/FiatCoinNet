CREATE TABLE [dbo].[Transactions]
(
	[TransactionId] INT NOT NULL , 
    [Amount] VARCHAR(1024) NOT NULL, 
    [CurrencyCode] NCHAR(10) NOT NULL, 
    [scriptSig] VARCHAR(1024) NOT NULL, 
    [scriptSigPubKey] VARCHAR(1024) NOT NULL, 
    [In_counter] INT NOT NULL, 
    [scriptPubKey] NCHAR(10) NOT NULL, 
    [Out_counter] INT NOT NULL, 
    [Source] VARCHAR(1024) NOT NULL, 
    [Dest] VARCHAR(1024) NOT NULL, 
    [IssuerId] INT NOT NULL, 
    [PreviousTransactionHash] VARCHAR(1024) NOT NULL, 
    [PreviousTransactionIndex] VARCHAR(1024) NOT NULL, 
    [MemoData] NVARCHAR(MAX) NOT NULL, 
    [InsertedDatetime] DATETIME NOT NULL, 
    [InsertedBy] NVARCHAR(64) NOT NULL, 
    PRIMARY KEY ([TransactionId])

)
