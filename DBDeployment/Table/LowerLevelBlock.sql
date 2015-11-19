CREATE TABLE [dbo].[LowerLevelBlock]
(
    [hash] VARCHAR(1024) NOT NULL, 
    [blockSize] INT NULL, 
    [blockHeader] VARCHAR(MAX) NULL, 
    [TransactionCounter] INT NULL, 
    [TransactionSet] VARCHAR(MAX) NULL, 
    [Epoch] INT NULL, 
    [Signature] VARCHAR(1024) NULL, 
    [SignatureToCertifyIssuer] VARCHAR(1024) NULL, 
    PRIMARY KEY ([hash])

)
