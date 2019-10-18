USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductPrice]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetProductPrice]
	@id int=NULL,
	@productId int=NULL,
	@segmentId int=NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT * from ProductsPrices ProductPrice
	INNER JOIN Segments Segment ON ProductPrice.Segment=Segment.SegmentId
	WHERE (ProductPrice.PriceId=@id or @id IS NULL)
	AND (ProductPrice.ProductId=@productId or @productId IS NULL)
	AND (ProductPrice.Segment=@segmentId or @segmentId IS NULL)
   
END
GO
