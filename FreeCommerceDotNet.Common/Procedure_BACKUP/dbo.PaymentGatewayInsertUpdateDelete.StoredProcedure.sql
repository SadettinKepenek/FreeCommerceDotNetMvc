USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[PaymentGatewayInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PaymentGatewayInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@PaymentId int=NULL,
	@PaymentName nvarchar(150)=NULL,
	@PaymentDescription nvarchar(500)=NULL
AS
BEGIN

	SET NOCOUNT ON;

	IF @Action='INSERT'
	BEGIN
		IF @PaymentName IS NOT NULL AND @PaymentDescription IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM PaymentGateways WHERE PaymentName=@PaymentName)
			BEGIN
				BEGIN TRY
					INSERT INTO PaymentGateways (PaymentName,PaymentDescription) VALUES (@PaymentName,@PaymentDescription)
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
				SET @Message='Verilen PaymentName zaten kayıt bulunmaktadır..'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END
	--UPDATE

	ELSE IF @Action='UPDATE'
	BEGIN
		IF @PaymentName IS NOT NULL AND @PaymentDescription IS NOT NULL AND @PaymentId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM PaymentGateways WHERE PaymentId=@PaymentId)
			BEGIN
				BEGIN TRY
					UPDATE PaymentGateways SET PaymentName=@PaymentName,PaymentDescription=@PaymentDescription WHERE PaymentId=@PaymentId
					SET @OutputValue=@PaymentId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen PaymentName zaten kayıt BULUNAMADI..'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END

	--DELETE

	ELSE IF @Action='DELETE'
	BEGIN
		IF @PaymentId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM PaymentGateways WHERE PaymentId=@PaymentId)
			BEGIN
				BEGIN TRY
					DELETE FROM PaymentGateways WHERE PaymentId=@PaymentId

					SET @OutputValue=@PaymentId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen PaymentName zaten kayıt BULUNAMADI..'
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
