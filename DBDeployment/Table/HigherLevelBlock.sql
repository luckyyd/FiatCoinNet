CREATE TABLE [dbo].[HigherLevelBlock]
(
	[Hash] VARCHAR(1024) NOT NULL PRIMARY KEY, 
    [blockSize] INT NOT NULL, 
    [blockHeader] VARCHAR(MAX) NOT NULL, 
    [LowerLevelBlockCounter] INT NOT NULL, 
    [LowerLevelBlockSet] VARCHAR(MAX) NOT NULL, 
    [Period] INT NOT NULL, 
    [Signature] VARCHAR(1024) NOT NULL
)
