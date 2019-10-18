USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[ProductInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProductInsertUpdateDelete]

	@Action nvarchar(50),
	@CategoryId int= NULL,
	@ProductId int = NULL,
	@Rating int = NULL,
	@ProductName nvarchar(200)= NULL,
	@ProductDescription nvarchar(500)= NULL,
	@MetatagTitle nvarchar(150)= NULL,
	@MetatagDescription nvarchar(150)= NULL,
	@MetatagKeywords nvarchar(150)= NULL,
	@ProductTags nvarchar(150)= NULL,
	@ProductCode nvarchar(150)= NULL,
	@ImageUrl nvarchar(150)= NULL,
	@Image1Url nvarchar(150)= NULL,
	@Image2Url nvarchar(150)= NULL,
	@Image3Url nvarchar(150)= NULL,
	@Image4Url nvarchar(150)= NULL,
	@SKU nvarchar(150)= NULL,
	@UPC nvarchar(150)= NULL,
	@EAN nvarchar(150)= NULL,
	@JAN nvarchar(150)= NULL,
	@ISBN nvarchar(150)= NULL,
	@MPN nvarchar(150)= NULL,
	@Quantity int= NULL,
	@OutOfStockStatus nvarchar(150)= NULL,
	@AvailableDate nvarchar(70)= NULL,
	@Rate int= NULL,
	@Length numeric(18,4)= NULL,
	@Width numeric(18,4)= NULL,
	@Height numeric(18,4)= NULL,
	@Weight numeric(18,4)= NULL,
	@Status bit= NULL,
	@Brand int  =NULL,
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output
	
AS
BEGIN
	
	SET NOCOUNT ON;
	
	-- CHECK IS VARIABLES NULL Statement

	IF @ImageUrl IS NULL
	BEGIN
		SET @ImageUrl='#'
	END
	IF @Image1Url IS NULL
	BEGIN
		SET @Image1Url='#'
	END
	IF @Image2Url IS NULL
	BEGIN
		SET @Image2Url='#'
	END
	IF @Image3Url IS NULL
	BEGIN
		SET @Image3Url='#'
	END
	IF @Image4Url IS NULL
	BEGIN
		SET @Image4Url='#'
	END

	-- INSERT STATEMENT

	IF @Action = 'INSERT'
	BEGIN
		IF @ProductName IS NOT NULL and @ProductDescription IS NOT NULL and @OutOfStockStatus IS NOT NULL and @AvailableDate IS NOT NULL and @Quantity IS NOT NULL 
		BEGIN
			IF NOT EXISTS (Select 1 From Products Where ProductName=@ProductName)
			BEGIN
				BEGIN TRY
					INSERT INTO Products(CategoryId,ProductName,ProductDescription,MetatagTitle,MetatagDescription,MetatagKeywords,ProductTags,ProductCode,ImageUrl,Image1Url,Image2Url,Image3Url,Image4Url,SKU,UPC,EAN,JAN,ISBN,MPN,Quantity,OutOfStockStatus,AvailableDate,Rate,Length,Width,Height,Weight,Status,Brand,Rating) 
					VALUES (@CategoryId,@ProductName,@ProductDescription,@MetatagTitle,@MetatagDescription,@MetatagKeywords,@ProductTags,@ProductCode,@ImageUrl,@Image1Url,@Image2Url,@Image3Url,@Image4Url,@SKU,@UPC,@EAN,@JAN,@ISBN,@MPN,@Quantity,@OutOfStockStatus,@AvailableDate,@Rate,@Length,@Width,@Height,@Weight,@Status,@Brand,@Rating)
					SET @OutputValue = SCOPE_IDENTITY()
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
				SET @Message = 'Kayıt Eklenirken Hata';
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
		BEGIN
			SET @OutputValue = -1
			SET @Message = 'Parametreler eksik girildi';
			Select @OutputValue AS ReturnValue,@Message as Message
		END	
	END
	
	--UPDATE STATEMENT

	ELSE IF @Action = 'UPDATE'
	BEGIN
		IF @ProductId IS NOT NULL and @ProductName IS NOT NULL and @ProductDescription IS NOT NULL and @OutOfStockStatus IS NOT NULL and @AvailableDate IS NOT NULL and @Quantity IS NOT NULL
		BEGIN
			IF EXISTS (Select 1 From Products Where ProductId=@ProductId)
			BEGIN
				BEGIN TRY
					UPDATE Products
					Set 
						CategoryId = @CategoryId,
						ProductName = @ProductName,
						ProductDescription = @ProductDescription,
						MetatagTitle = @MetatagTitle,
						MetatagDescription = @MetatagDescription,
						MetatagKeywords= @MetatagKeywords,
						ProductTags= @ProductTags,
						ProductCode= @ProductCode,
						Rating = @Rating,
						ImageUrl= @ImageUrl,
						Image1Url= @Image1Url,
						Image2Url= @Image2Url,
						Image3Url= @Image3Url,
						Image4Url= @Image4Url,
						SKU = @SKU,
						UPC = @UPC,
						EAN = @EAN,
						JAN = @JAN,
						ISBN = @ISBN,
						MPN = @MPN,
						Quantity = @Quantity,
						OutOfStockStatus = @OutOfStockStatus,
						AvailableDate = @AvailableDate,
						Rate = @Rate,
						Length = @Length,
						Width = @Width,
						Height = @Height,
						Weight = @Weight,
						Status = @Status,
						Brand = @Brand
					Where ProductId =@ProductId
					SET @OutputValue = @ProductId
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
				SET @Message = 'Verilen ProductId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message

			END
		END
		ELSE
		BEGIN
				SET @OutputValue = -1
				SET @Message = 'Parametreler eksik girilmiş'
				Select @OutputValue AS ReturnValue,@Message as Message
		END
	END







	ELSE IF @Action = 'DELETE'
	BEGIN
		IF @ProductId IS NOT NULL 
		BEGIN
			IF EXISTS(SELECT 1 FROM Products WHERE ProductId=@ProductId)
			BEGIN
				BEGIN TRY
					Delete From Products Where ProductId=@ProductId
					SET @OutputValue = @ProductId
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
				SET @Message = 'Verilen ProductId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'ProductId Parametreleri Verilmemiştir.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		RETURN
	END
END
		

GO
