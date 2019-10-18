USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[CustomerInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CustomerInsertUpdateDelete]
	@Action		nvarchar(50),
	@Address1	nvarchar(500) = NULL, 	
	@Password	nvarchar(150) = NULL, 	
	@Telephone	nvarchar(150) = NULL, 	
	@Email		nvarchar(150) = NULL, 	
	@Lastname	nvarchar(150) = NULL, 	
	@Firstname	nvarchar(150) = NULL, 	
	@CustomerId	int = NULL, 	
	@Address2	nvarchar(500) = NULL, 	
	@Status		bit = NULL, 	
	@TaxAddress	nvarchar(500) = NULL, 	
	@SegmentId	int = NULL, 	
	@UserId		int	 = NULL, 
	@OutputValue int=NULL output,
	@Message	nvarchar(500)=NULL output

	
AS
BEGIN
	
	SET NOCOUNT ON;
	
	
	-- INSERT STATEMENT

	IF @Action = 'INSERT'
	BEGIN
		IF @Firstname IS NOT NULL and @Lastname IS NOT NULL and @Email IS NOT NULL and @Password IS NOT NULL and @Status IS NOT NULL and @Address1 IS NOT NULL and @Telephone IS NOT NULL
		BEGIN
			IF NOT EXISTS (Select 1 From Customers Where Email=@Email )
			BEGIN
				BEGIN TRY
					INSERT INTO Customers (Address1,Address2,Password,Telephone,Email,Lastname,Firstname,Status,TaxAddress,SegmentId,UserId) VALUES (@Address1,@Address2,@Password,@Telephone,@Email,@Lastname,@Firstname,@Status,@TaxAddress,@SegmentId,@UserId)
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
				SET @Message = 'Kayıt Zaten Mevcut'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
		BEGIN
			SET @OutputValue = -1
			SET @Message = ' Parametreler Eksik Girilmiş.'
			Select @OutputValue AS ReturnValue,@Message as Message
		END	
		
	END


	-- UPDATE STATEMENT

	ELSE IF @Action = 'UPDATE'
	BEGIN
		IF @CustomerId IS NOT NULL and @Firstname IS NOT NULL and @Lastname IS NOT NULL and @Email IS NOT NULL and @Password IS NOT NULL and @Status IS NOT NULL and @UserId IS NOT NULL and @Address1 IS NOT NULL and @Telephone IS NOT NULL
		BEGIN
			IF EXISTS (Select 1 From Customers Where CustomerId = @CustomerId)
			BEGIN
				BEGIN TRY
					UPDATE Customers
					Set 
						Firstname=@Firstname,
						Lastname=@Lastname,
						Address1=@Address1,
						Address2=@Address2,
						Telephone = @Telephone,
						Password = @Password,
						TaxAddress = @TaxAddress,
						Status = @Status,
						SegmentId = @SegmentId,
						UserId = @UserId,
						Email = @Email
					Where CustomerId=@CustomerId
					SET @OutputValue = @CustomerId
					SET @Message = 'Success'
					Select @OutputValue AS ReturnValue,@Message as Message
				END TRY
				BEGIN CATCH
					SET @OutputValue = -1
					SET @Message =  ERROR_MESSAGE();
					Select @OutputValue AS ReturnValue,@Message as Message
				END CATCH
			END
			ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Verilen CustomerId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message

			END
		END
		ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Parametreler Eksik Girilmiş'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		RETURN
	END
	

	-- DELETE STATEMENT

	ELSE IF @Action = 'DELETE'
	BEGIN
		IF @CustomerId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Customers WHERE CustomerId=@CustomerId)
			BEGIN
				BEGIN TRY
					Delete From Customers Where CustomerId=@CustomerId
					SET @OutputValue = 0
					SET @Message = 'Success'
					Select @OutputValue AS ReturnValue,@Message as Message
				END TRY
				BEGIN CATCH
					SET @OutputValue = -1
					SET @Message =  ERROR_MESSAGE();
					Select @OutputValue AS ReturnValue,@Message as Message
				END CATCH
			END
			ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'Verilen CustomerId parametresi için kayıt bulunmamaktadır..'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		END
		ELSE
			BEGIN
				SET @OutputValue = -1
				SET @Message = 'CustomerId Parametreleri Verilmemiştir.'
				Select @OutputValue AS ReturnValue,@Message as Message
			END
		RETURN
	END
END
GO
