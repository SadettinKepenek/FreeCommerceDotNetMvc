USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrder]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetOrder]
@OrderId int=NULL,
	@CustomerId int=NULL,
	@ShippingId int=NULL,
	@PaymentId int=NULL,
	@BrandId int=NULL,
	@StartPrice numeric(18,4)=NULL,
	@EndPrice numeric(18,4)=NULL,
	@DiscountedOrders bit=NULL,
	@ProductId int=NULL,
	@SegmentId int=NULL,
	@OrderDate nvarchar(200)=NULL,
	@DeliveryDate nvarchar(200)=NULL,
	@DeliveryStatus nvarchar(200)=NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT 
	OrderMaster.OrderId,
	OrderMaster.OrderDate,
	OrderMaster.TrackNumber,
	OrderMaster.DeliveryDate,
	OrderMaster.DeliveryComment,
	OrderMaster.DeliveryStatus,
	Customer.CustomerId,
	Customer.Firstname CustomerFirstname,
	Customer.Lastname CustomerLastname,
	Customer.Email CustomerEmail,
	Customer.Address1 CustomerAddress1,
	Customer.Address2 CustomerAddress2,
	Customer.TaxAddress CustomerTaxAddress,
	Customer.Status CustomerStatus,
	PaymentGateway.PaymentId,
	PaymentGateway.PaymentName,
	PaymentGateway.PaymentDescription,
	Shipping.ShippingId,
	Shipping.ShippingName,
	Shipping.ShippingDescription,
	OrderDetail.OrderDetailId,
	Product.ProductId,
	Product.ImageUrl ProductImageUrl,
	OrderDetail.Quantity ProductQuantity,
	OrderDetail.OrderDetailId OrderDetailId,
	OrderDetail.ProductPrice,
	OrderDetail.IsDiscountedPrice IsDiscountedPrice,
	Product.ProductName,
	Product.ProductCode,
	Brand.BrandId ProductBrandId,
	Brand.BrandName ProductBrandName,
	Brand.BrandUrl BrandUrl
	
	
	
	FROM OrdersMaster OrderMaster
	INNER JOIN Customers Customer ON OrderMaster.CustomerId=Customer.CustomerId
	INNER JOIN Segments CustomerSegment ON Customer.SegmentId=CustomerSegment.SegmentId
	INNER JOIN PaymentGateways PaymentGateway ON OrderMaster.PaymentGatewayId=PaymentGateway.PaymentId
	INNER JOIN Shippings Shipping ON OrderMaster.ShippingId=Shipping.ShippingId
	INNER JOIN OrdersDetail OrderDetail ON OrderMaster.OrderId=OrderDetail.OrderId
	INNER JOIN Products Product ON OrderDetail.ProductId=Product.ProductId
	LEFT JOIN Brands Brand ON Product.Brand=Brand.BrandId
	WHERE (OrderMaster.OrderId=@OrderId OR @OrderId IS NULL)
	AND (OrderMaster.CustomerId=@CustomerId OR @CustomerId IS NULL)
	AND (OrderMaster.OrderDate=@OrderDate OR @OrderDate IS NULL)
	AND (OrderMaster.DeliveryDate=@DeliveryDate OR @DeliveryDate IS NULL)
	AND (OrderMaster.DeliveryStatus=@DeliveryStatus OR @DeliveryStatus IS NULL)
	AND (Customer.SegmentId=@SegmentId OR @SegmentId IS NULL)
	AND (OrderMaster.ShippingId=@ShippingId OR @ShippingId IS NULL)
	AND (OrderMaster.PaymentGatewayId=@PaymentId OR @PaymentId IS NULL)
	AND (Brand.BrandId=@BrandId OR @BrandId IS NULL)
	AND ((OrderDetail.ProductPrice BETWEEN @StartPrice AND @EndPrice ) OR (@StartPrice IS NULL AND @EndPrice IS NULL))
	AND (OrderDetail.IsDiscountedPrice=@DiscountedOrders OR @DiscountedOrders IS NULL)
	AND (OrderDetail.ProductId=@ProductId OR @ProductId IS NULL)
END
GO
