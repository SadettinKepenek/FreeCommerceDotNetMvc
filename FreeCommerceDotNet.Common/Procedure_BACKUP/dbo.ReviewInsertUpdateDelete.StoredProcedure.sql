USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[ReviewInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ReviewInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@ReviewId int=NULL,
	@CustomerId int=NULL,
	@ProductId int=NULL,
	@Title nvarchar(150)=NULL,
	@Comment nvarchar(250)=NULL,
	@Rating int=NULL,
	@PublishDate nvarchar(70)=NULL,
	@LikeCount int=NULL,
	@DislikeCount int=NULL,
	@Status bit=NULL
AS
BEGIN
	SET NOCOUNT ON;

	-- Check Variables IS NULL?


	IF @LikeCount IS NULL
	BEGIN
		SET @LikeCount=0
	END
	IF @DislikeCount IS NULL
	BEGIN
		SET @DislikeCount=0
	END
	IF @Status IS NULL
	BEGIN
		SET @Status=1
	END
	IF @PublishDate IS NULL
	BEGIN
		SET @PublishDate=GETDATE()
	END
	IF @Rating IS NULL
	BEGIN
		SET @Rating=5
	END

	-- INSERT STATEMENTS

	IF @Action='INSERT'
	BEGIN
		IF @Title IS NOT NULL and @Comment IS NOT NULL AND @CustomerId IS NOT NULL AND @ProductId IS NOT NULL
		BEGIN
			
				BEGIN TRY
					INSERT INTO Reviews(Title,Comment,LikeCount,DislikeCount,PublishDate,Rating,ProductId,CustomerId,Status) VALUES (@Title,@Comment,@LikeCount,@DislikeCount,@PublishDate,@Rating,@ProductId,@CustomerId,@Status)
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
			SET @Message = 'Fonksiyon için Gerekli Parametreler Verilmemiştir.'
			Select @OutputValue AS ReturnValue,@Message as Message
		END	
	END

	-- UPDATE STATEMENTS

	ELSE IF @Action='UPDATE'
		IF @Title IS NOT NULL and @Comment IS NOT NULL AND @CustomerId IS NOT NULL AND @ProductId IS NOT NULL AND @ReviewId IS NOT NULL
		BEGIN
			IF EXISTS (Select 1 From Reviews Where ReviewId=@ReviewId)
			BEGIN
				BEGIN TRY
					Update Reviews Set
					ProductId=@ProductId,
					CustomerId=@CustomerId,
					Title=@Title,
					Comment=@Comment,
					Status=@Status,
					Rating=@Rating,
					PublishDate=@PublishDate,
					LikeCount=@LikeCount,
					DislikeCount=@DislikeCount
					where ReviewId=@ReviewId
					
					SET @OutputValue = @ReviewId
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
				SET @Message = 'Verilen ReviewId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
		BEGIN
			SET @OutputValue = -1
			SET @Message = 'Fonksiyon için Gerekli Parametreler Verilmemiştir.'
			Select @OutputValue AS ReturnValue,@Message as Message
	END	

	ELSE IF @Action='DELETE'
	BEGIN
		IF @ReviewId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Reviews WHERE ReviewId=@ReviewId)
			BEGIN
				BEGIN TRY
					Delete From Reviews Where ReviewId=@ReviewId
					SET @OutputValue = 0
					SET @Message = 'Success'
					Select @OutputValue AS ReturnValue,@Message as Message
				END TRY
				BEGIN CATCH
					SET @OutputValue = -1
					SET @Message = 'Kayıt Silinirken hata oluştu'
					Select @OutputValue AS ReturnValue,@Message as Message
				END CATCH
			END
			ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Verilen ReviewId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Fonksiyon için gerekli Parametreler Verilmemiştir.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		RETURN
	END

		


    
END
GO
