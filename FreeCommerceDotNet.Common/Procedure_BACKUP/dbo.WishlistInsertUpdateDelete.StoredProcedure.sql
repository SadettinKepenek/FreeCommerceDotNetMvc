USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[WishlistInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[WishlistInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@WishlistId int=NULL,
	@CustomerId int=NULL,
	@ProductId int=NULL,
	@WishDate nvarchar(50)=NULL
AS
BEGIN


	SET NOCOUNT ON;
	--bunu silmemiz lazım delete için wishdate gerekmiyor insert le update e ekelyek
	IF @WishDate IS NULL 
	BEGIN
		Set @WishDate=GETDATE();
	END

	IF @Action='INSERT'
	BEGIN
		IF @ProductId IS NOT NULL AND @CustomerId IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM Wishlists WHERE ProductId=@ProductId and CustomerId=@CustomerId)
			BEGIN
				BEGIN TRY --refreshledim gelmedi
					INSERT INTO Wishlists (ProductId,CustomerId,WishRequestDate) VALUES (@ProductId,@CustomerId,@WishDate)
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
				SET @Message='Verilen bilgiler için kayıt bulunmaktadır'
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
		IF @WishlistId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Wishlists WHERE wishlistId=@WishlistId)
			BEGIN
				BEGIN TRY

					UPDATE Wishlists SET  ProductId=@ProductId,CustomerId=@CustomerId,WishRequestDate=@WishDate
					WHERE wishlistId=@WishlistId


					SET @OutputValue=@WishlistId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen parametreler için kayıt bulunmaktadır'
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
		IF @WishlistId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Wishlists WHERE wishlistId=@WishlistId)
			BEGIN
				BEGIN TRY

					DELETE FROM Wishlists
					WHERE wishlistId=@WishlistId


					SET @OutputValue=@WishlistId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					Set @OutputValue=-1
					Set @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE BEGIN
				SET @OutputValue=-1
				SET @Message='Verilen parametreler için zaten kayıt bulunmamaktadır'
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
