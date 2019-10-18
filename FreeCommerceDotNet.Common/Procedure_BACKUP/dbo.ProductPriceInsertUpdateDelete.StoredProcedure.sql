USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[ProductPriceInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProductPriceInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@PriceId int=NULL,
	@ProductId int=NULL,
	@Price numeric(18,4)=NULL,
	@Segment int=NULL
AS
BEGIN

	SET NOCOUNT ON;

	-- INSERT STATEMENTS

	IF @Action='INSERT'
	BEGIN
		IF @Price IS NOT NULL AND @ProductId IS NOT NULL AND @Segment IS NOT NULL 
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM ProductsPrices WHERE ProductId=@ProductId and Segment=@Segment)
			BEGIN
				BEGIN TRY
					
					INSERT INTO ProductsPrices (Price,ProductId,Segment)
					VALUES (@Price,@ProductId,@Segment)


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
				SET @Message='Verilen Product ve Segment için zaten kayıt bulunmaktadır..'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END

	-- UPDATE STATEMENTS

	ELSE IF @Action='UPDATE'
	BEGIN
		IF @Price IS NOT NULL AND @ProductId IS NOT NULL AND @Segment IS NOT NULL  AND @PriceId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM ProductsPrices WHERE PriceId=@PriceId)
			BEGIN
				BEGIN TRY
					
					UPDATE ProductsPrices SET 
					Price=@Price,
					ProductId=@ProductId,
					Segment=@Segment
					WHERE PriceId=@PriceId

					SET @OutputValue=@PriceId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen Product ve Segment için zaten kayıt bulunamadı..'
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
		IF  @PriceId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM ProductsPrices WHERE PriceId=@PriceId)
			BEGIN
				BEGIN TRY
					
					DELETE FROM ProductsPrices
					WHERE PriceId=@PriceId

					SET @OutputValue=@PriceId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen Product ve Segment için zaten kayıt bulunamadı..'
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
