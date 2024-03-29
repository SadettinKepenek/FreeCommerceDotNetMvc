USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[UserInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UserInsertUpdateDelete] 
	@Action nvarchar(50),
	@UserId int =NULL,
	@Username nvarchar(150) =NULL,
	@Password nvarchar(150) = NULL,
	@EMail nvarchar(150) = NULL,
	@Role nvarchar(150)=NULL,
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output
AS
BEGIN
	SET NOCOUNT ON

	-- Check IS Role NULL

	IF @Role IS NULL
	BEGIN
		SET @Role='Client'
	END

	-- INSERT STATEMENT


	IF @Action = 'INSERT'
	BEGIN
		IF @Username IS NOT NULL AND @Password IS NOT NULL AND @EMail IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM Users WHERE Username=@Username)
			BEGIN
				BEGIN TRY
					INSERT INTO Users (Username,Password,EMail,Role) VALUES (@Username,@Password,@EMail,@Role)
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
				SET @Message = 'Verilen username parametresi için kayıt bulunmaktadır.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
		BEGIN
			SET @OutputValue = -1
			SET @Message = 'Fonksiyon için beklenen parametreler gönderilmedi.'
			Select @OutputValue AS ReturnValue,@Message as Message
		END
	END

	-- UPDATE STATEMENT

	ELSE IF @Action = 'UPDATE'
	BEGIN
	IF  @UserId IS NOT NULL and @Username IS NOT NULL AND @Password IS NOT NULL AND @EMail IS NOT NULL 
	BEGIN
		IF EXISTS(SELECT 1 FROM Users WHERE Username=@Username)
		BEGIN
			BEGIN TRY

				UPDATE USERS SET 
					Username=@Username,
					Password=@Password,
					EMail=@EMail,
					Role=@Role
				WHERE UserId=@UserId

				SET @OutputValue = @UserId
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
			SET @Message = 'Verilen username parametresi için kayıt bulunmamaktadır..'
			Select @OutputValue AS ReturnValue,@Message as Message
		END
	END
	ELSE
	BEGIN
		SET @OutputValue = -1
		SET @Message = 'Fonksiyon için beklenen parametreler gönderilmedi.'
		Select @OutputValue AS ReturnValue,@Message as Message
	END
	END

	ELSE IF @Action = 'DELETE'
	BEGIN
	IF @UserId IS NOT NULL
	BEGIN
		IF EXISTS(SELECT 1 FROM Users WHERE UserId=@UserId)
		BEGIN
			BEGIN TRY			

				DELETE FROM Users WHERE UserId=@UserId

				SET @OutputValue = @UserId
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
			SET @Message = 'Verilen USERID parametresi için kayıt bulunmamaktadır..'
			Select @OutputValue AS ReturnValue,@Message as Message
		END
	END
	ELSE
	BEGIN
		SET @OutputValue = -1
		SET @Message = 'Fonksiyon için beklenen parametreler gönderilmedi.'
		Select @OutputValue AS ReturnValue,@Message as Message
	END
	END


END
GO
