USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[OrderMasterInsertUpdateDelete]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OrderMasterInsertUpdateDelete]
	@OutputValue int=NULL output,
	@Message nvarchar(500)=NULL output,
	@Action nvarchar(50),
	@OrderId int=Null,
	@PaymentGatewayId int=Null,
	@ShippingId int=Null,
	@CustomerId int=Null,
	@TrackNumber nvarchar(200)=NULL,
	@OrderDate nvarchar(200)=NULL,
	@DeliveryDate nvarchar(200)=NULL,
	@DeliveryComment nvarchar(200)=NULL,
	@DeliveryStatus nvarchar(200)=NULL
AS
BEGIN

	SET NOCOUNT ON;


	IF @DeliveryStatus IS NULL
	BEGIN
		SET @DeliveryStatus = 0
	END

	

	-- INSERT

	IF @Action='INSERT'
	BEGIN
		IF @PaymentGatewayId IS NOT NULL AND @ShippingId IS NOT NULL AND @CustomerId IS NOT NULL AND @OrderDate IS NOT NULL
		BEGIN
			BEGIN
				BEGIN TRY
					INSERT INTO OrdersMaster (ShippingId,PaymentGatewayId,CustomerId,OrderDate,DeliveryDate,DeliveryComment,DeliveryStatus,TrackNumber)
					VALUES (@ShippingId,@PaymentGatewayId,@CustomerId,@OrderDate,@DeliveryDate,@DeliveryComment,@DeliveryStatus,@TrackNumber)

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
		IF @PaymentGatewayId IS NOT NULL AND @ShippingId IS NOT NULL AND @CustomerId IS NOT NULL AND @OrderDate IS NOT NULL AND @OrderId IS NOT NULL
		BEGIN
			BEGIN
				IF EXISTS(SELECT 1 FROM OrdersMaster WHERE OrderId=@OrderId)
				BEGIN
				
					BEGIN TRY
						UPDATE OrdersMaster SET ShippingId=@ShippingId
								,PaymentGatewayId=@PaymentGatewayId
								,CustomerId=@CustomerId
								,OrderDate=@OrderDate
								,DeliveryDate=@DeliveryDate
								,DeliveryComment=@DeliveryComment
								,DeliveryStatus=@DeliveryStatus
								,TrackNumber=@TrackNumber
						WHERE OrderId=@OrderId

						SET @OutputValue=@OrderId
						SET @Message='Success'
					END TRY
					BEGIN CATCH
						Set @OutputValue=-1
						Set @Message=ERROR_MESSAGE()
					END CATCH
				END
				ELSE 
				BEGIN
					SET @OutputValue=-1
					SET @Message='Verilen OrderID için kayıt bulunamadı'
				END
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
		IF @OrderId IS NOT NULL
		BEGIN
			BEGIN
				IF EXISTS(SELECT 1 FROM OrdersMaster WHERE OrderId=@OrderId)
				BEGIN
				
					BEGIN TRY
						DELETE FROM OrdersMaster
						WHERE OrderId=@OrderId

						SET @OutputValue=@OrderId
						SET @Message='Success'
					END TRY
					BEGIN CATCH
						Set @OutputValue=-1
						Set @Message=ERROR_MESSAGE()
					END CATCH
				END
				ELSE 
				BEGIN
					SET @OutputValue=-1
					SET @Message='Verilen OrderID için kayıt bulunamadı'
				END
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
