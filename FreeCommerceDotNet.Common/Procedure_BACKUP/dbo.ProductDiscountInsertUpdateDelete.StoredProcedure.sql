USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[ProductDiscountInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProductDiscountInsertUpdateDelete]
	@Action nvarchar(50),
	@ProductId	int	= NULL,
	@DiscountId	int	= NULL,
	@StartDate	nvarchar(100)	= NULL,
	@EndDate	nvarchar(100)	= NULL,
	@NewPrice	numeric(18, 4)	= NULL,
	@Quantity	int	= NULL,
	@Segment	int	= NULL,
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output

	
AS
BEGIN
	
	SET NOCOUNT ON;
		
	-- INSERT STATEMENT

	IF @Action = 'INSERT'
	BEGIN
		IF @ProductId IS NOT NULL and @NewPrice IS NOT NULL and @Segment IS NOT NULL and @StartDate IS NOT NULL and @EndDate IS NOT NULL and @Quantity IS NOT NULL
		BEGIN
			IF NOT EXISTS (Select 1 From ProductsDiscounts Where ProductId = @ProductId)
			BEGIN
				BEGIN TRY
					INSERT INTO ProductsDiscounts(ProductId,StartDate,EndDate,Quantity,NewPrice,Segment) VALUES (@ProductId,@StartDate, @EndDate,@Quantity,@NewPrice,@Segment)
					SET @OutputValue = SCOPE_IDENTITY()
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
				SET @Message = 'Verilen ProductId parametresi için kayıt bulunmaktadır.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
		BEGIN
			SET @OutputValue = -1
			SET @Message = 'Parametreler Eksik Girilmiş'
			Select @OutputValue AS ReturnValue,@Message as Message
		END	
		
	END


	-- UPDATE STATEMENT

	ELSE IF @Action = 'UPDATE'
	BEGIN
		IF @DiscountId IS NOT NULL and @ProductId IS NOT NULL and @NewPrice IS NOT NULL and @Segment IS NOT NULL and @StartDate IS NOT NULL and @EndDate IS NOT NULL and @Quantity IS NOT NULL
		BEGIN
			IF EXISTS (Select 1 From ProductsDiscounts Where DiscountId=@DiscountId)
			BEGIN
				BEGIN TRY
					UPDATE ProductsDiscounts
					Set 
						ProductId=@ProductId,
						StartDate=@StartDate,
						EndDate=@EndDate,
						Quantity=@Quantity
					Where DiscountId=@DiscountId
					SET @OutputValue = @DiscountId
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
				SET @Message = 'Verilen DiscountId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message

			END
		END
		ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Parametreler Eksik Girilmiş'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		RETURN
	END
	

	-- DELETE STATEMENT

	ELSE IF @Action = 'DELETE'
	BEGIN
		IF @DiscountId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM ProductsDiscounts WHERE DiscountId=@DiscountId)
			BEGIN
				BEGIN TRY
					Delete From ProductsDiscounts Where DiscountId=@DiscountId
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
				SET @Message = 'Verilen DiscountId parametresi için kayıt bulunmamaktadır..'
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
