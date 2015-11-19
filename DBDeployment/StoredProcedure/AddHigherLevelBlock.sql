CREATE PROCEDURE dbo.AddHigherLevelBlock
	@hash VARCHAR(1024), 
    @blockSize INT, 
    @blockHeader VARCHAR(MAX), 
    @LowerLevelBlockCounter INT, 
    @LowerLevelBlockSet VARCHAR(MAX), 
    @Period INT, 
    @Signature VARCHAR(1024)
AS
BEGIN

	INSERT INTO [dbo].[HigherLevelBlock]
    (
		[hash], [blockSize], [blockHeader], [LowerLevelBlockCounter], [LowerLevelBlockSet], [Period], [Signature]
    )
    VALUES
    (
		@hash,
		@blockSize,
		@blockHeader,
		@LowerLevelBlockCounter,
		@LowerLevelBlockSet,
		@Period,
		@Signature
    )
END
