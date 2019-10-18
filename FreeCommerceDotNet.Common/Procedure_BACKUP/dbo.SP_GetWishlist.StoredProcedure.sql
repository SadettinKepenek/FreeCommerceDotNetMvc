USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetWishlist]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetWishlist]
	@WishlistId int = NULL,
	@ProductId int = NULL,
	@CustomerId int = NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT 	
	Product.ProductId ProductId,
	Product.ProductName ProductName,
	Product.ImageUrl ProductImageUrl,
	Product.Status ProductStatus,
	Product.Quantity ProductQuantity,
	Customer.CustomerId CustomerId,
	Whislist.wishlistId WhisId,
	Whislist.WishRequestDate WishDate
	
	FROM Wishlists Whislist
	INNER JOIN Products Product ON Whislist.ProductId = Product.ProductId
	INNER JOIN Customers Customer ON Whislist.CustomerId = Customer.CustomerId

	WHERE
	(Whislist.wishlistId =@wishlistId OR @wishlistId IS NULL)
	AND	(Customer.CustomerId = @CustomerId OR @CustomerId IS NULL)

END
GO
