USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[AttributeGroupInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AttributeGroupInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@AttributeGroupId int=NULL,
	@AttributeGroupName nvarchar(200)=NULL
AS
BEGIN
	
	SET NOCOUNT ON;
		IF @Action='INSERT'
	BEGIN
		IF @AttributeGroupName IS NOT NULL 
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM AttributeGroups WHERE AttributeGroupName=@AttributeGroupName)
			BEGIN
				BEGIN TRY
					INSERT INTO AttributeGroups (AttributeGroupName) VALUES (@AttributeGroupName)
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
				SET @Message='Verilen AttributeGroupName için zaten kayıt bulunmaktadır..'
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
		IF @AttributeGroupName IS NOT NULL AND @AttributeGroupId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM AttributeGroups WHERE AttributeGroupId=@AttributeGroupId)
			BEGIN
				BEGIN TRY

					UPDATE AttributeGroups SET  AttributeGroupName=@AttributeGroupName
					WHERE AttributeGroupId=@AttributeGroupId


					SET @OutputValue=@AttributeGroupId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen @AttributeGroupId için kayıt bulunmamaktadır'
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
		IF @AttributeGroupId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM AttributeGroups WHERE AttributeGroupId=@AttributeGroupId)
			BEGIN
				BEGIN TRY

					DELETE FROM AttributeGroups
					WHERE AttributeGroupId=@AttributeGroupId


					SET @OutputValue=@AttributeGroupId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen AttributeGroupName için Attribute Groupda zaten kayıt bulunmamaktadır'
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
