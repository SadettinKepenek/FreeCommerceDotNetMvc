USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[AttributeInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AttributeInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@AttributeId int=NULL,
	@AttributeName nvarchar(150) =NULL,
	@AttributeGroup int=NULL
AS
BEGIN

	SET NOCOUNT ON;

	IF @Action='INSERT'
	BEGIN
		IF @AttributeName IS NOT NULL AND @AttributeGroup IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM Attributes WHERE AttributeName=@AttributeName and AttributeGroup=@AttributeGroup)
			BEGIN
				BEGIN TRY
					INSERT INTO Attributes (AttributeName,AttributeGroup) VALUES (@AttributeName,@AttributeGroup)
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
				SET @Message='Verilen AttributeName için Attribute Groupda zaten kayıt bulunmaktadır..'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END

	-- UPDATE

	ELSE IF @Action='UPDATE'
	BEGIN
		IF @AttributeName IS NOT NULL AND @AttributeGroup IS NOT NULL AND @AttributeId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Attributes WHERE AttributeId=@AttributeId)
			BEGIN
				BEGIN TRY

					UPDATE Attributes SET  AttributeName=@AttributeName,AttributeGroup=@AttributeGroup
					WHERE AttributeId=@AttributeId


					SET @OutputValue=@AttributeId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen AttributeName için Attribute Groupda zaten kayıt bulunmamaktadır'
			END
		END
		ELSE
		BEGIN
			SET @OutputValue=-1
			SET @Message='Fonksiyon için gerekli parametreler verilmemiştir.'
					
		END
		Select @OutputValue AS ReturnValue,@Message as Message
	END
	
	-- DELETE

	ELSE IF @Action='DELETE'
	BEGIN
		IF @AttributeId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Attributes WHERE AttributeId=@AttributeId)
			BEGIN
				BEGIN TRY

					DELETE FROM Attributes
					WHERE AttributeId=@AttributeId


					SET @OutputValue=@AttributeId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen AttributeName için Attribute Groupda zaten kayıt bulunmamaktadır'
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
