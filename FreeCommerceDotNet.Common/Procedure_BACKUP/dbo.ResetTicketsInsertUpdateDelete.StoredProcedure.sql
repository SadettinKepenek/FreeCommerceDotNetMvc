USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[ResetTicketsInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ResetTicketsInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@TicketId int = NULL,
	@UserId int = NULL
AS
BEGIN

	SET NOCOUNT ON;

	IF @Action='INSERT'
	BEGIN
		IF @UserId IS NOT NULL
		BEGIN

			BEGIN
				BEGIN TRY
					INSERT INTO ResetTickets (UserId) VALUES (@UserId)
					SET @OutputValue=SCOPE_IDENTITY()
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
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
		IF @TicketId IS NOT NULL 
		BEGIN
			BEGIN
				BEGIN TRY

					UPDATE ResetTickets SET  tokenUsed = 1
					WHERE TicketId=@TicketId

					SET @OutputValue=@TicketId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
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
		IF @TicketId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM ResetTickets WHERE TicketId=@TicketId)
			BEGIN
				BEGIN TRY

					DELETE FROM ResetTickets
					WHERE TicketId=@TicketId


					SET @OutputValue=@TicketId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen parametreler için kayıt bulunmamaktadır'
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
