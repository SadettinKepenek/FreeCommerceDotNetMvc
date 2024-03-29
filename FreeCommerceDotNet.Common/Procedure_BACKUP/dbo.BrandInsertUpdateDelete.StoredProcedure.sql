USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[BrandInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BrandInsertUpdateDelete]

	@Action nvarchar(50),
	@BrandId int = NULL,
	@BrandName nvarchar(150)=NULL,
	@BrandDescription nvarchar(500)=NULL,
	@BrandUrl nvarchar(150)=NULL,
	@BrandImageUrl nvarchar(150)=NULL,
	@OutputValue int=NULL output,
	@Message int=NULL output

	
AS
BEGIN
	
	SET NOCOUNT ON;
	
	-- CHECK IS VARIABLES NULL Statement

	IF @BrandUrl IS NULL
	BEGIN
		SET @BrandUrl='#'
	END
	IF @BrandImageUrl IS NULL
	BEGIN
		SET @BrandImageUrl='#'
	END

	-- INSERT STATEMENT

	IF @Action = 'INSERT'
	BEGIN
		IF @BrandName IS NOT NULL and @BrandDescription IS NOT NULL
		BEGIN
			IF NOT EXISTS (Select 1 From Brands Where BrandName=@BrandName)
			BEGIN
				BEGIN TRY
					INSERT INTO Brands (BrandName,BrandDescription,BrandUrl,BrandImageUrl) VALUES (@BrandName,@BrandDescription,@BrandUrl,@BrandImageUrl)
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
		IF (@BrandId IS NOT NULL AND @BrandName IS NOT NULL and @BrandDescription IS NOT NULL)
		BEGIN
			IF EXISTS (Select 1 From Brands Where BrandId=@BrandId)
			BEGIN
				BEGIN TRY
					UPDATE Brands
					Set 
						BrandName=@BrandName,
						BrandDescription=@BrandDescription,
						BrandUrl=@BrandUrl,
						BrandImageUrl=@BrandImageUrl
					Where BrandId=@BrandId
					SET @OutputValue = @BrandId
					SET @Message = 'Success'
					Select @OutputValue AS ReturnValue,@Message as Message
				END TRY
				BEGIN CATCH
					SET @OutputValue = -1
					SET @Message = 'Kayıt Güncellenirken hata oluştu'
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
		IF @BrandId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Brands WHERE BrandId=@BrandId)
			BEGIN
				BEGIN TRY
					Delete From Brands Where BrandId=@BrandId
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
