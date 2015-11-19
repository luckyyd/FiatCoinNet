CREATE PROCEDURE dbo.AddLowerLevelBlock
	@hash VARCHAR(1024), 
    @blockSize INT, 
    @blockHeader VARCHAR(MAX), 
    @TransactionCounter INT, 
    @TransactionSet VARCHAR(MAX), 
    @Epoch INT, 
    @Signature VARCHAR(1024), 
    @SignatureToCertifyIssuer VARCHAR(1024)
AS
BEGIN

	INSERT INTO [dbo].[LowerLevelBlock]
    (
		[hash], [blockSize], [blockHeader], [TransactionCounter], [TransactionSet], [Epoch], [Signature], [SignatureToCertifyIssuer]
    )
    VALUES
    (
		@hash,
		@blockSize,
		@blockHeader,
		@TransactionCounter,
		@TransactionSet,
		@Epoch,
		@Signature,
		@SignatureToCertifyIssuer
    )
END
