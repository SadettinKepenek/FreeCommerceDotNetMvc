USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[OrderDetailDiscountInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OrderDetailDiscountInsertUpdateDelete]
	@Action nvarchar(50) = NULL,
	@IsDiscountedPrice	bit	= NULL,
	@ProductPrice	numeric(18, 4)	= NULL,
	@Quantity	int	= NULL,
	@OrderId	int	= NULL,
	@ProductId	int	= NULL,
	@OrderDetailId	int	= NULL,
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output

	
AS
BEGIN
	
	SET NOCOUNT ON;
		
	-- INSERT STATEMENT

	IF @Action = 'INSERT'
	BEGIN
		IF @ProductPrice IS NOT NULL and @Quantity IS NOT NULL and @OrderId IS NOT NULL and @ProductId IS NOT NULL
		BEGIN
			IF NOT EXISTS (Select 1 From OrdersDetail Where OrderId = @OrderId and ProductId=@ProductId)
			BEGIN
				BEGIN TRY
					INSERT INTO OrdersDetail(ProductId,OrderId,Quantity,ProductPrice,IsDiscountedPrice) VALUES (@ProductId,@OrderId,@Quantity,@ProductPrice,@IsDiscountedPrice) 
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
				SET @Message = 'Verilen OrderId parametresi için kayıt bulunmaktadır.'
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
	IF @ProductPrice IS NOT NULL and @Quantity IS NOT NULL and @OrderId IS NOT NULL and @ProductId IS NOT NULL
		BEGIN
			IF EXISTS (Select 1 From OrdersDetail Where OrderDetailId=@OrderDetailId)
			BEGIN
				BEGIN TRY
					UPDATE OrdersDetail
					Set 
						ProductId=@ProductId,
						OrderId=@OrderId,
						ProductPrice=@ProductPrice,
						Quantity=@Quantity,
						IsDiscountedPrice = @IsDiscountedPrice
					Where OrderDetailId = @OrderDetailId
					SET @OutputValue = @OrderDetailId
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
				SET @Message = 'Verilen OrderDetailId parametresi için kayıt bulunmamaktadır..'
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
		IF @OrderDetailId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM OrdersDetail WHERE OrderDetailId=@OrderDetailId)
			BEGIN
				BEGIN TRY
					Delete From OrdersDetail Where OrderDetailId=@OrderDetailId
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
				SET @Message = 'Verilen OrderDetailId parametresi için kayıt bulunmamaktadır..'
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
