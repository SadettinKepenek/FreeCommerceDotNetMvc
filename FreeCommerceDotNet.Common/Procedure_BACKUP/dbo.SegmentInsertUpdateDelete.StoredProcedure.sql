USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SegmentInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentInsertUpdateDelete]

	@Action nvarchar(50),
	@SegmentId int = NULL,
	@SegmentName nvarchar(150)=NULL,
	@Priorty nvarchar(150)=NULL,	
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output

	
AS
BEGIN
	
	SET NOCOUNT ON;
		
	-- INSERT STATEMENT

	IF @Action = 'INSERT'
	BEGIN
		IF @SegmentName IS NOT NULL and @Priorty IS NOT NULL
		BEGIN
			IF NOT EXISTS (Select 1 From Segments Where SegmentName=@SegmentName)
			BEGIN
				BEGIN TRY
					INSERT INTO Segments (SegmentName,Priorty) VALUES (@SegmentName,@Priorty)
					SET @OutputValue = SCOPE_IDENTITY()
					SET @Message = 'Success'
					Select @OutputValue AS ReturnValue,@Message as Message
				END TRY
				BEGIN CATCH
					SET @OutputValue = -1
					SET @Message = 'Kayıt eklenirken hata oluştu'
					Select @OutputValue AS ReturnValue,@Message as Message
				END CATCH
				
			END
			ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Verilen SegmentName parametresi için kayıt bulunmaktadır.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
		BEGIN
			SET @OutputValue = -1
			SET @Message = 'SegmentName ve Priorty Parametreleri Verilmemiştir.'
			Select @OutputValue AS ReturnValue,@Message as Message
		END	
		
	END
	-- UPDATE STATEMENT

	ELSE IF @Action = 'UPDATE'
	BEGIN
		IF (@SegmentId IS NOT NULL AND @SegmentName IS NOT NULL and @Priorty IS NOT NULL)
		BEGIN
			IF EXISTS (Select 1 From Segments Where SegmentId=@SegmentId)
			BEGIN
				BEGIN TRY
					UPDATE Segments
					Set 
						SegmentName=@SegmentName,
						Priorty=@Priorty
					Where SegmentId=@SegmentId
					SET @OutputValue = @SegmentId
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
				SET @Message = 'Verilen SegmentId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message

			END
		END
		ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'SegmentId,SegmentName veya Priorty Parametreleri Verilmemiştir.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		RETURN
	END
	-- DELETE STATEMENT

	ELSE IF @Action = 'DELETE'
	BEGIN
		IF @SegmentId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Segments WHERE SegmentId=@SegmentId)
			BEGIN
				BEGIN TRY
					Delete From Segments Where SegmentId = @SegmentId
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
				SET @Message = 'Verilen SegmentId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'SegmentId Parametreleri Verilmemiştir.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		RETURN
	END
END
GO
