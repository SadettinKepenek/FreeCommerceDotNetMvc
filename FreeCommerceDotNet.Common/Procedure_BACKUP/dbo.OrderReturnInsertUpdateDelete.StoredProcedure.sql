USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[OrderReturnInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OrderReturnInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@ReturnId	int	=NULL ,
	@OrderId	int	=NULL ,
	@ProductId	int	=NULL ,
	@BoxOpened	bit	=NULL ,
	@ReturnStatus	bit	=NULL ,
	@ReturnReason	nvarchar(500)	=NULL ,
	@Comment	nvarchar(500)	=NULL ,
	@ReturnResponse	nvarchar(500)	=NULL 
AS
BEGIN

	SET NOCOUNT ON;

	IF @ReturnStatus IS NULL
	BEGIN
		SET @ReturnStatus=0
	END

	-- INSERT STATEMENTS

	IF @Action='INSERT'
	BEGIN
		IF @OrderId IS NOT NULL AND @ProductId IS NOT NULL AND @BoxOpened IS NOT NULL AND @ReturnStatus IS NOT NULL AND @ReturnReason IS NOT NULL AND
		@Comment IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM OrdersReturns WHERE OrderId=@OrderId AND ProductId=@ProductId)
			BEGIN
				BEGIN TRY
					
					INSERT INTO OrdersReturns (OrderId,ProductId,Comment,ReturnReason,ReturnResponse,ReturnStatus,BoxOpened)
					VALUES (@OrderId,@ProductId,@Comment,@ReturnReason,@ReturnResponse,@ReturnStatus,@BoxOpened)

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
				SET @Message='Verilen PRODUCT VE SIPARIS ICIN zaten kayıt bulunmaktadır..'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END
	
	--UPDATE STATEMENTS

	ELSE IF @Action='UPDATE'
	BEGIN
		IF @OrderId IS NOT NULL AND @ProductId IS NOT NULL AND @BoxOpened IS NOT NULL AND @ReturnStatus IS NOT NULL AND @ReturnReason IS NOT NULL AND
		@Comment IS NOT NULL AND @ReturnId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM OrdersReturns WHERE ReturnId=@ReturnId)
			BEGIN
				BEGIN TRY
					
					UPDATE OrdersReturns SET OrderId=@OrderId
					,ProductId=@ProductId
					,Comment=@Comment
					,ReturnReason=@ReturnReason
					,ReturnResponse=@ReturnResponse
					,ReturnStatus=@ReturnStatus
					,BoxOpened=@BoxOpened
					WHERE ReturnId=@ReturnId

					SET @OutputValue=@ReturnId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen PRODUCT VE SIPARIS ICIN zaten kayıt BULUNAMADI..'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END

	-- DELETE STATEMENTS

	ELSE IF @Action='DELETE'
	BEGIN
		IF @ReturnId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM OrdersReturns WHERE ReturnId=@ReturnId)
			BEGIN
				BEGIN TRY
					
					DELETE FROM OrdersReturns WHERE ReturnId=@ReturnId

					SET @OutputValue=@ReturnId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen PRODUCT VE SIPARIS ICIN zaten kayıt BULUNAMADI..'
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
