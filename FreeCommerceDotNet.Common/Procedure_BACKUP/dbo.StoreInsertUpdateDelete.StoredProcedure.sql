USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[StoreInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[StoreInsertUpdateDelete]

	@Action		nvarchar(50),
	@MetaTitle	nvarchar(200) = NULL,
	@MetaTagDescription	nvarchar(1000) = NULL,
	@StoreId	int	= NULL,
	@MetaTagKeywords	nvarchar(500) = NULL,
	@StoreName	nvarchar(200) = NULL,
	@Phone	nvarchar(100) = NULL,
	@StoreOwner	nvarchar(200) = NULL,
	@Address	nvarchar(400) = NULL,
	@EMail	nvarchar(200) = NULL,
	@CellPhone	nvarchar(100) = NULL,
	@Fax	nvarchar(100) = NULL,
	@ImageUrl	nvarchar(300) = NULL,
	@OpeningTimes	nvarchar(300) = NULL,
	@Comment	nvarchar(300) = NULL,
	@AllowReviews	bit	= NULL,
	@MaxLoginAttempts	int	= NULL,
	@LoginDisplayPrices	bit	= NULL,
	@DisplayPricesWithTax	bit	= NULL,
	@DisplayStock	bit	= NULL,
	@ShowOutOfStockWarning	bit	= NULL,
	@OutputValue int=NULL output,
	@Message	nvarchar(500)=NULL output

	
AS
BEGIN
	
	SET NOCOUNT ON;
	
	-- CHECK IS VARIABLES NULL Statement

	-- INSERT STATEMENT

	IF @Action = 'INSERT'
	BEGIN
		IF @StoreName IS NOT NULL and @StoreOwner IS NOT NULL and @Email IS NOT NULL and @Phone IS NOT NULL and @Address IS NOT NULL
		BEGIN
			IF NOT EXISTS (Select 1 From Stores Where StoreName=@StoreName)
			BEGIN
				BEGIN TRY
					INSERT INTO Stores(MetaTitle,MetaTagDescription,MetaTagKeywords,StoreName,StoreOwner,Address,EMail,Phone,CellPhone,Fax,ImageUrl,OpeningTimes,Comment,AllowReviews,DisplayPricesWithTax,LoginDisplayPrices,MaxLoginAttempts,DisplayStock,ShowOutOfStockWarning) VALUES (@MetaTitle,@MetaTagDescription,@MetaTagKeywords,@StoreName,@StoreOwner,@Address,@EMail,@Phone,@CellPhone,@Fax,@ImageUrl,@OpeningTimes,@Comment,@AllowReviews,@DisplayPricesWithTax,@LoginDisplayPrices,@MaxLoginAttempts,@DisplayStock,@ShowOutOfStockWarning)
					SET @Message = 'Success'
					Select @OutputValue AS ReturnValue,@Message as Message
				END TRY
				BEGIN CATCH
					SET @OutputValue = -1
					SET @Message = ERROR_MESSAGE()
					Select @OutputValue AS ReturnValue,@Message as Message
				END CATCH
				
			END
			ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Verilen BrandName parametresi için kayıt bulunmaktadır.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
		BEGIN
			SET @OutputValue = -1
			SET @Message = 'BrandName ve BrandDescription Parametreleri Verilmemiştir.'
			Select @OutputValue AS ReturnValue,@Message as Message
		END	
		
	END


	-- UPDATE STATEMENT

	ELSE IF @Action = 'UPDATE'
	BEGIN
		IF @StoreName IS NOT NULL and @StoreOwner IS NOT NULL and @Email IS NOT NULL and @Phone IS NOT NULL and @Address IS NOT NULL
		BEGIN
			IF EXISTS (Select 1 From Stores Where StoreId = @StoreId)
			BEGIN
				BEGIN TRY
					UPDATE Stores
					Set 
						MetaTitle = @MetaTitle,
						MetaTagDescription = @MetaTagDescription,
						MetaTagKeywords = @MetaTagKeywords,
						StoreName = @StoreName,
						StoreOwner = @StoreOwner,
						Address = @Address,
						EMail = @EMail,
						Phone = @Phone,
						CellPhone = @CellPhone,
						Fax = @Fax,
						ImageUrl = @ImageUrl,
						OpeningTimes = @OpeningTimes,
						Comment = @Comment,
						AllowReviews = @AllowReviews,
						DisplayPricesWithTax = @DisplayPricesWithTax,
						LoginDisplayPrices = @LoginDisplayPrices,
						MaxLoginAttempts = @MaxLoginAttempts,
						DisplayStock = @DisplayStock,
						ShowOutOfStockWarning = @ShowOutOfStockWarning
					Where StoreId=@StoreId
					SET @OutputValue = @StoreId
					SET @Message = 'Success'
					Select @OutputValue AS ReturnValue,@Message as Message
				END TRY
				BEGIN CATCH
					SET @OutputValue = -1
					SET @Message = ERROR_MESSAGE();
					Select @OutputValue AS ReturnValue,@Message as Message
				END CATCH
			END
			ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Verilen BrandId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message

			END
		END
		ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'BrandId,BrandName veya BrandDescription Parametreleri Verilmemiştir.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		RETURN
	END
	

	-- DELETE STATEMENT

	ELSE IF @Action = 'DELETE'
	BEGIN
		IF @StoreId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Stores WHERE StoreId=@StoreId)
			BEGIN
				BEGIN TRY
					Delete From Brands Where BrandId=@StoreId
					SET @OutputValue = 0
					SET @Message = 'Success'
					Select @OutputValue AS ReturnValue,@Message as Message
				END TRY
				BEGIN CATCH
					SET @OutputValue = -1
					SET @Message = ERROR_MESSAGE();
					Select @OutputValue AS ReturnValue,@Message as Message
				END CATCH
			END
			ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Verilen BrandId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'BrandId Parametreleri Verilmemiştir.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		RETURN
	END
END
GO
