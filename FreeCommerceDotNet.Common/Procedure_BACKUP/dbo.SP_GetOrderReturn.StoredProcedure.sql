USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrderReturn]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetOrderReturn]
	@ReturnId int=NULL,
	@CustomerId int=NULL,
	@OrderId int=NULL,
	@ProductId int=Null,
	@ShippingId int =NULL,
	@PaymentId int=NULL,
	@BrandId int=NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		Select 
				OrderReturn.ReturnId,
				OrderReturn.ReturnReason,
				OrderReturn.ReturnResponse,
				OrderReturn.ReturnStatus,
				OrderReturn.OrderId,
				OrderReturn.ProductId,
				OrderReturn.BoxOpened,
				OrderReturn.Comment,
				Product.ProductName,
				Product.ProductCode,
				Brand.BrandId,
				Brand.BrandName,
				OrderMaster.OrderDate,
				OrderMaster.DeliveryDate,
				OrderMaster.DeliveryStatus,
				OrderMaster.DeliveryComment,
				OrderMaster.TrackNumber,
				Customer.CustomerId,
				Customer.Firstname CustomerFirstname,
				Customer.Lastname CustomerLastname,
				Customer.Email CustomerEmail,
				Customer.Telephone CustomerPhone,
				Customer.Address1 CustomerAddress1,
				Customer.Address2 CustomerAddress2,
				Customer.TaxAddress CustomerTaxAddress,
				Customer.UserId CustomerUserId,
				PaymentGateway.PaymentId,
				PaymentGateway.PaymentName,
				Shipping.ShippingId,
				Shipping.ShippingName

		from OrdersReturns OrderReturn
		INNER JOIN OrdersMaster OrderMaster ON OrderReturn.OrderId=OrderMaster.OrderId
		INNER JOIN Customers Customer ON OrderMaster.CustomerId=Customer.CustomerId
		INNER JOIN PaymentGateways PaymentGateway ON OrderMaster.PaymentGatewayId=PaymentGateway.PaymentId
		INNER JOIN Shippings Shipping ON OrderMaster.ShippingId=Shipping.ShippingId
		INNER JOIN Products Product ON OrderReturn.ProductId=Product.ProductId
		LEFT JOIN Brands Brand ON Product.Brand=Brand.BrandId

		WHERE (OrderReturn.ReturnId=@ReturnId OR @ReturnId IS NULL)
		AND (Customer.CustomerId=@CustomerId OR @CustomerId IS NULL)
		AND (PaymentGateway.PaymentId=@PaymentId OR @PaymentId IS NULL)
		AND (Shipping.ShippingId=@ShippingId OR @ShippingId IS NULL)
		AND (Product.ProductId=@ProductId OR @ProductId IS NULL)
		AND (OrderMaster.OrderId=@OrderId OR @OrderId IS NULL)
		AND (Brand.BrandId =@BrandId OR @BrandId IS NULL)
END
GO
