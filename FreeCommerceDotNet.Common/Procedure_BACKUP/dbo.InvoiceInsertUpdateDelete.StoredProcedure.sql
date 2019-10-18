USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[InvoiceInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InvoiceInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@TranscationNo nvarchar(50)=NULL,
	@OrderId int = NULL,
	@InvoiceId int = NULL,
	@InvoiceStatus bit = NULL,
	@InvoiceTotalPrice  numeric(18,4) = NULL,
	@InvoiceTotalDiscount  numeric(18,4) = NULL
AS
BEGIN

	SET NOCOUNT ON;

	IF @Action='INSERT'
	BEGIN
		IF @TranscationNo IS NOT NULL AND @OrderId IS NOT NULL AND @InvoiceTotalPrice IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM Invoices WHERE TranscationNo=@TranscationNo)
			BEGIN
				BEGIN TRY
					INSERT INTO Invoices (OrderId,InvoiceTotalPrice,InvoiceStatus,InvoiceTotalDiscount,TranscationNo) VALUES (@OrderId,@InvoiceTotalPrice,@InvoiceStatus,@InvoiceTotalDiscount,@TranscationNo)
					SET @OutputValue=SCOPE_IDENTITY()
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen parametreler için zaten kayıt bulunmaktadır..'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END

	-- UPDATE

	ELSE IF @Action='UPDATE'
	BEGIN
		IF @InvoiceId IS NOT NULL AND @OrderId IS NOT NULL AND @InvoiceTotalPrice IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Invoices WHERE InvoiceId=@InvoiceId)
			BEGIN
				BEGIN TRY

					UPDATE Invoices SET
					OrderId=@OrderId,
					InvoiceTotalPrice=@InvoiceTotalPrice,
					InvoiceTotalDiscount = @InvoiceTotalDiscount,
					InvoiceStatus = @InvoiceStatus,
					TranscationNo=@TranscationNo
					

					WHERE InvoiceId=@InvoiceId


					SET @OutputValue=@InvoiceId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen parametreler için zaten kayıt bulunmamaktadır'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END
	
	-- DELETE

	ELSE IF @Action='DELETE'
	BEGIN
		IF @InvoiceId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Invoices WHERE InvoiceId=@InvoiceId)
			BEGIN
				BEGIN TRY

					DELETE FROM Invoices
					WHERE InvoiceId=@InvoiceId


					SET @OutputValue=@InvoiceId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen parametre için zaten kayıt bulunmamaktadır'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END


END
GO
