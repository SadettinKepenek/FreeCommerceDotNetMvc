USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[CategoryInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CategoryInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@CategoryId	int=NULL,
	@ParentId	int=NULL,
	@CategoryName	nvarchar(300)=NULL,
	@Description	nvarchar(2000)=NULL,
	@MetatagTitle	nvarchar(300)=NULL,
	@MetatagDescription	nvarchar(2000)=NULL,
	@Metatagkeywords	nvarchar(300)=NULL,
	@ImageUrl	nvarchar(300)=NULL,
	@ShowNavbar	bit	=NULL,
	@isActive	bit	=NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	IF @ParentId IS NULL
	BEGIN
		SET @ParentId=-1
	END

	IF @MetatagTitle IS NULL
	BEGIN
		SET @MetatagTitle=@CategoryName
	END
	
	IF @MetatagDescription IS NULL
	BEGIN
		SET @MetatagDescription=@Description
	END

	IF @ShowNavbar IS NULL
	BEGIN
		SET @ShowNavbar = 1
	END

	IF @isActive IS NULL
	BEGIN 
		SET @isActive = 1
	END

	IF @ImageUrl IS NULL
	BEGIN
		SET @ImageUrl='#'
	END

	-- INSERT STATEMENTS

	IF @Action='INSERT'
	BEGIN
		IF @CategoryName IS NOT NULL AND @Description IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM Categories WHERE CategoryName=@CategoryName)
			BEGIN
				BEGIN TRY
					INSERT INTO Categories (CategoryName,Description,MetatagTitle,MetatagDescription,Metatagkeywords,ImageUrl,isActive,ShowNavbar,ParentId) VALUES 
					(@CategoryName,@Description,@MetatagTitle,@MetatagDescription,@Metatagkeywords,@ImageUrl,@isActive,@ShowNavbar,@ParentId)
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
				SET @Message='Verilen CategoryName zaten kayıt bulunmaktadır..'
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
		IF @CategoryName IS NOT NULL AND @Description IS NOT NULL AND @CategoryId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Categories WHERE CategoryId=@CategoryId)
			BEGIN
				BEGIN TRY

					UPDATE Categories SET 
					CategoryName=@CategoryName
					,Description=@Description
					,MetatagTitle=@MetatagTitle
					,MetatagDescription=@MetatagDescription
					,Metatagkeywords=@Metatagkeywords
					,ImageUrl=@ImageUrl
					,isActive=@isActive
					,ShowNavbar=@ShowNavbar
					,ParentId=@ParentId
					WHERE CategoryId=@CategoryId

					SET @OutputValue=@CategoryId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen CategoryName zaten kayıt bulunamadı..'
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
		IF  @CategoryId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Categories WHERE CategoryId=@CategoryId)
			BEGIN
				BEGIN TRY

					DELETE FROM Categories
					WHERE CategoryId=@CategoryId

					SET @OutputValue=@CategoryId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen CategoryName zaten kayıt bulunamadı..'
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
