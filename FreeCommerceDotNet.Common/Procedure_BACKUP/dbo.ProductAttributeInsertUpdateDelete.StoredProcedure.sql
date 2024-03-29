USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[ProductAttributeInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProductAttributeInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@RelationId int=NULL,
	@ProductId int=NULL,
	@AttributeId int=NULL,
	@AttributeDescription nvarchar(150)=NULL
AS
BEGIN

	SET NOCOUNT ON;

	-- INSERT STATEMENTS

	IF @Action='INSERT'
	BEGIN
		IF @ProductId IS NOT NULL AND @AttributeId IS NOT NULL AND @AttributeDescription IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM ProductsAttributes where ProductId=@ProductId and AttributeId=@AttributeId)
			BEGIN
				BEGIN TRY
					INSERT INTO ProductsAttributes
					(AttributeId,ProductId,AttributeDescription)
					VALUES (@AttributeId,@ProductId,@AttributeDescription)


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
				SET @Message='Verilen Product ve Attribute için zaten kayıt bulunmaktadır..'
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
		IF @ProductId IS NOT NULL AND @AttributeId IS NOT NULL AND @AttributeDescription IS NOT NULL AND @RelationId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM ProductsAttributes where RelationId=@RelationId)
			BEGIN
				BEGIN TRY
					UPDATE ProductsAttributes SET
					AttributeId=@AttributeId,ProductId=@ProductId,AttributeDescription=@AttributeDescription
					WHERE RelationId=@RelationId


					SET @OutputValue=@RelationId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen Product ve Attribute için zaten kayıt BULUNAMADI..'
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
		IF @RelationId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM ProductsAttributes where RelationId=@RelationId)
			BEGIN
				BEGIN TRY
					DELETE FROM ProductsAttributes
					WHERE RelationId=@RelationId


					SET @OutputValue=@RelationId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen Product ve Attribute için zaten kayıt BULUNAMADI..'
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
