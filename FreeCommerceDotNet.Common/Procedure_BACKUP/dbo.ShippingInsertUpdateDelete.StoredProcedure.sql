USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[ShippingInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ShippingInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@ShippingId int=NULL,
	@ShippingName nvarchar(150)=NULL,
	@ShippingDescription nvarchar(150)=NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	--Update

    IF @Action='INSERT'
	BEGIN
		IF @ShippingName IS NOT NULL AND @ShippingDescription IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM Shippings WHERE ShippingName=@ShippingName)
			BEGIN
				BEGIN TRY
					INSERT INTO Shippings (ShippingName,ShippingDescription) VALUES (@ShippingName,@ShippingDescription)
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
				SET @Message='Verilen ShippingName zaten kayıt bulunmaktadır..'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END

	--	Update

	ELSE IF @Action='UPDATE'
	BEGIN
		IF @ShippingName IS NOT NULL AND @ShippingDescription IS NOT NULL AND @ShippingId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Shippings WHERE ShippingId=@ShippingId)
			BEGIN
				BEGIN TRY

					UPDATE Shippings SET
					ShippingName=@ShippingName,
					ShippingDescription=@ShippingDescription
					WHERE ShippingId=@ShippingId
				
					SET @OutputValue=@ShippingId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen ShippingId için kayıt bulunamadı'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END

	ELSE IF @Action='DELETE'
	BEGIN
		IF  @ShippingId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Shippings WHERE ShippingId=@ShippingId)
			BEGIN
				BEGIN TRY

					Delete From Shippings
					WHERE ShippingId=@ShippingId
				
					SET @OutputValue=@ShippingId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen ShippingId için kayıt bulunamadı'
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
