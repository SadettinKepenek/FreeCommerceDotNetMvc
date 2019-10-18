USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[CouponInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CouponInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@CouponId int=NULL,
	@CouponStartDate nvarchar(100)=NULL,
	@CouponEndDate nvarchar(100)=NULL,
	@CouponDiscount nvarchar(50)=NULL,
	@CouponQuantity int=NULL,
	@CouponStatus bit=NULL

AS
BEGIN
	SET NOCOUNT ON;

	IF @CouponStatus IS NULL
	BEGIN
		SET @CouponStatus=1
	END

	-- INSERT STATEMENTS
	
	IF @Action='INSERT'
	BEGIN
		IF @CouponStartDate IS NOT NULL AND @CouponEndDate IS NOT NULL AND @CouponQuantity IS NOT NULL AND @CouponDiscount IS NOT NULL
		BEGIN
			BEGIN TRY
				
				INSERT INTO Coupons (CouponStartDate,CouponEndDate,CouponDiscount,CouponQuantity,CouponStatus)
				VALUES (@CouponStartDate,@CouponEndDate,@CouponDiscount,@CouponQuantity,@CouponStatus)

				SET @OutputValue=SCOPE_IDENTITY()
				SET @Message='Success'

			END TRY
			BEGIN CATCH
				SET @OutputValue=-1
				SET @Message=ERROR_MESSAGE()
			END CATCH
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
		IF @CouponStartDate IS NOT NULL AND @CouponEndDate IS NOT NULL AND @CouponQuantity IS NOT NULL AND @CouponDiscount IS NOT NULL AND @CouponId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Coupons WHERE CouponId=@CouponId)
			BEGIN
			
				BEGIN TRY
				
					UPDATE Coupons SET CouponStartDate=@CouponStartDate,CouponEndDate=@CouponEndDate,CouponDiscount=@CouponDiscount,CouponQuantity=@CouponQuantity,CouponStatus=@CouponStatus
					WHERE CouponId=@CouponId
				

					SET @OutputValue=@CouponId
					SET @Message='Success'
				END TRY
				BEGIN CATCH
					SET @OutputValue=-1
					SET @Message=ERROR_MESSAGE()
					END CATCH
			END
			ELSE
			BEGIN
				SET @OutputValue=@CouponId
				SET @Message='Verilen CouponId için Kayıt bulunamadı.'
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
		IF @CouponId IS NOT NULL
		BEGIN
			IF EXISTS(SELECT 1 FROM Coupons WHERE CouponId=@CouponId)
			BEGIN
			
				BEGIN TRY
				
					DELETE FROM Coupons WHERE CouponId=@CouponId
					SET @OutputValue=@CouponId
					SET @Message='Success'

				END TRY
				BEGIN CATCH
					SET @OutputValue=-1
					SET @Message=ERROR_MESSAGE()
				END CATCH
			END
			ELSE
			BEGIN
				SET @OutputValue=@CouponId
				SET @Message='Verilen CouponId için Kayıt bulunamadı.'
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
