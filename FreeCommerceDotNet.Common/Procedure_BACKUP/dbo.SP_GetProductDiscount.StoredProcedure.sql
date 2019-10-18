USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductDiscount]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetProductDiscount]
	@id int=NULL,
	@productId int=NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * FROM ProductsDiscounts ProductDiscount
	INNER JOIN Segments Segment ON ProductDiscount.Segment=Segment.SegmentId
	WHERE (ProductDiscount.DiscountId=@id or @id IS NULL)
	AND (ProductDiscount.ProductId=@productId or @productId IS NULL)

END
GO
