CREATE PROCEDURE [dbo].[AddTransaction]
    --@issuerId INT,
    --@source VARCHAR(1024),
    --@dest VARCHAR(1024),
    --@amount MONEY,
    --@currencyCode CHAR(3),
    --@memoData NVARCHAR(MAX)
	@transactionId VARCHAR(1024),
	@amount VARCHAR(1024),
	@currencyCode CHAR(3),
	@scriptSig VARCHAR(1024),
	@scriptSigPubKey VARCHAR(1024),
	@in_counter INT,
	@scriptPubKey VARCHAR(1024),
	@out_counter INT,
	@source VARCHAR(1024),
	@dest VARCHAR(1024),
	@issuerId INT,
	@PreviousTransactionHash VARCHAR(1024),
	@PreviousTransactionIndex VARCHAR(1024),
    @memoData NVARCHAR(MAX)
AS
BEGIN
    DECLARE @dt_now DATETIME = GETUTCDATE()
    DECLARE @vc_inserted_by VARCHAR(128) = SUSER_SNAME()

    INSERT INTO [dbo].[Transactions]
    (
        --[IssuerId], [Source], [Dest], [Amount], [CurrencyCode], [MemoData], [InsertedDatetime], [InsertedBy]
		[TransactionId], [Amount], [CurrencyCode], [scriptSig], [scriptSigPubKey],
		[In_counter],[scriptPubKey], [Out_counter], [Source], [Dest], [IssuerId],
		[PreviousTransactionHash], [PreviousTransactionIndex], [MemoData], [InsertedDatetime], [InsertedBy]
    )
    VALUES
    (
        --@issuerId,
        --@source,
        --@dest,
        --@amount,
        --@currencyCode,
        --@memoData,
        --@dt_now,
        --@vc_inserted_by
		@transactionId,
		@amount,
		@currencyCode,
		@scriptSig,
		@scriptSigPubKey,
		@in_counter,
		@scriptPubKey,
		@out_counter,
		@source,
		@dest,
		@issuerId,
		@PreviousTransactionHash,
		@PreviousTransactionIndex,
		@memoData,
		@dt_now,
		@vc_inserted_by
    )

    --SELECT * FROM [dbo].[PaymentTransaction]
    --WHERE [IssuerId] = @issuerId
    --    AND [Source] = @source
    --    AND [Dest] = @dest
    --    AND [InsertedDatetime] = @dt_now
	SELECT * FROM [dbo].[Transactions]
    WHERE [TransactionId] = @transactionId
		AND	[IssuerId] = @issuerId
        AND [Source] = @source
        AND [Dest] = @dest
        AND [InsertedDatetime] = @dt_now

END
