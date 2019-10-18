USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProduct]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetProduct]
	@id int=NULL,
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
	SELECT  Product.*,
		Category.CategoryName,
		Category.Metatagkeywords CategorySEOKeywords,
		Brand.BrandName,
		Brand.BrandDescription,
		Brand.BrandUrl,
		Brand.BrandImageUrl,
		ProductPrice.PriceId,
		ProductPrice.Price,
		ProductDiscount.DiscountId AS DiscountId,
		ProductDiscount.StartDate DiscountStartDate,
		ProductDiscount.EndDate DiscountEndDate,
		ProductDiscount.Quantity DiscountQuantity,
		ProductDiscount.NewPrice DiscountNewPrice,
		Segment.SegmentId,
		Segment.SegmentName SegmentName,
		ProductAttribute.RelationId ProductAttributeRelationId,
		ProductAttribute.AttributeDescription ProductAttributeDescription,
		Attribute.AttributeId,
		Attribute.AttributeName,
		AttributeGroup.AttributeGroupId,
		AttributeGroup.AttributeGroupName
		
	FROM Products Product

	INNER JOIN Categories Category ON Product.CategoryId = Category.CategoryId
	LEFT JOIN Brands Brand on Product.Brand=Brand.BrandId
	INNER JOIN ProductsPrices ProductPrice on Product.ProductId = ProductPrice.ProductId
	INNER JOIN Segments Segment On ProductPrice.Segment=Segment.SegmentId
	LEFT JOIN ProductsDiscounts ProductDiscount ON Product.ProductId=ProductDiscount.ProductId AND Segment.SegmentId=ProductDiscount.Segment 
	LEFT JOIN ProductsAttributes ProductAttribute ON Product.ProductId = ProductAttribute.ProductId
	LEFT JOIN Attributes Attribute ON ProductAttribute.AttributeId=Attribute.AttributeId
	LEFT JOIN AttributeGroups AttributeGroup ON Attribute.AttributeGroup=AttributeGroup.AttributeGroupId
	WHERE (Product.ProductId=@id or @id IS NULL)
	AND ((Category.CategoryId=@CategoryId OR Category.ParentId=@CategoryId) OR @CategoryId IS NULL ) 
	AND (Product.Brand=@BrandId OR @BrandId IS  NULL)
	AND (Product.Quantity <> 0 OR @JustInStock IS  NULL)
	AND (Segment.SegmentId=@SegmentId OR @SegmentId IS  NULL)
	AND ((ProductPrice.Price BETWEEN @ProductStartPrice AND @ProductEndPrice) OR (@ProductStartPrice IS  NULL AND @ProductEndPrice IS  NULL))
	AND (ProductDiscount.DiscountId IS NOT NULL OR @JustInDiscount IS  NULL)	
END
GO
