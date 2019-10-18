USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCategory]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetCategory]
	@CategoryId int=NULL,
	@BrandId int = NULL,
	@ProductStartPrice numeric(18,4) = NULL,
	@ProductEndPrice numeric(18,4) = NULL,
	@JustInStock bit=NULL,
	@JustInDiscount bit=NULL,
	@SegmentId int=NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT 
		Category.CategoryId,
		Category.ParentId,
		Category.CategoryName,
		Category.Description,
		Category.MetatagTitle,
		Category.MetatagDescription,
		Category.Metatagkeywords,
		Category.ImageUrl,
		Category.ShowNavbar,
		Category.isActive,

		SubCategory.CategoryId SubCategoryId,
		SubCategory.ParentId SubCategoryParentId,
		SubCategory.CategoryName SubCategoryName,
		SubCategory.Description SubCategoryDescription,
		SubCategory.MetatagTitle SubCategoryMetaTitle,
		SubCategory.MetatagDescription SubCategoryMetaDescription,
		SubCategory.Metatagkeywords SubCategoryMetaKeywords,
		SubCategory.ImageUrl SubCategoryImageUrl,
		SubCategory.ShowNavbar SubCategoryShowNavbar,
		SubCategory.isActive SubCategoryIsActive,
		Brand.BrandId,
		Brand.BrandName,
		Product.ProductId,
		Product.ProductName,
		Product.ProductCode,
		Product.ImageUrl ProductImageUrl,
		Product.Image1Url ProductImage1Url,
		Product.OutOfStockStatus ProductOutOfStockStatus,
		Product.Quantity ProductQuantity,
		Product.Status ProductStatus,
		Product.Rate ProductRate,
		ProductDiscount.DiscountId DiscountId,
		ProductDiscount.StartDate DiscountStartDate,
		ProductDiscount.EndDate DiscountEndDate,
		ProductDiscount.Quantity DiscountQuantity,
		Product.CategoryId ProductCategoryId,
		Segment.SegmentId,
		Segment.SegmentName,
		ProductPrice.PriceId PriceId,
		ProductPrice.Price ProductPrice


	FROM Categories Category
	LEFT JOIN Categories SubCategory ON Category.CategoryId=SubCategory.ParentId
	INNER JOIN Products Product ON Category.CategoryId=Product.CategoryId
	LEFT JOIN Brands Brand ON Product.Brand=Brand.BrandId
	INNER JOIN ProductsPrices ProductPrice ON Product.ProductId = ProductPrice.ProductId
	INNER JOIN Segments Segment On ProductPrice.Segment=Segment.SegmentId
	LEFT JOIN ProductsDiscounts ProductDiscount ON Product.ProductId = ProductDiscount.ProductId AND Segment.SegmentId=ProductDiscount.Segment
	WHERE 
	((Category.CategoryId=@CategoryId OR Category.ParentId=@CategoryId) OR (@CategoryId IS NULL) ) 
	AND (Product.Brand=@BrandId OR @BrandId IS  NULL)
	AND (Product.Quantity <> 0 OR @JustInStock IS  NULL)
	AND (Segment.SegmentId=@SegmentId OR @SegmentId IS  NULL)
	AND ((ProductPrice.Price BETWEEN @ProductStartPrice AND @ProductEndPrice) OR (@ProductStartPrice IS  NULL AND @ProductEndPrice IS  NULL))
	AND (ProductDiscount.DiscountId IS NOT NULL OR @JustInDiscount IS  NULL)	
END
GO
