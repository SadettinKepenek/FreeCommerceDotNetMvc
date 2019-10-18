USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchProduct]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_SearchProduct]
	@ProductName nvarchar(200) = NULL
AS 
BEGIN
	DECLARE @WORD1 nvarchar(10)
	DECLARE @WORD2 nvarchar(10)
	DECLARE @WORD3 nvarchar(10)
	DECLARE @WORD4 nvarchar(10)
	DECLARE @WORD5 nvarchar(10)
	DECLARE @WORD6 nvarchar(10)
	DECLARE @WORD7 nvarchar(10)

	SET @ProductName=RTRIM(@ProductName)

	SET @WORD1=(parsename(replace(@ProductName,' ','.'),1))
	SET @WORD2=(parsename(replace(@ProductName,' ','.'),2))
	SET @WORD3=(parsename(replace(@ProductName,' ','.'),3))
	SET @WORD4=(parsename(replace(@ProductName,' ','.'),4))
	SET @WORD5=(parsename(replace(@ProductName,' ','.'),5))
	SET @WORD6=(parsename(replace(@ProductName,' ','.'),6))
	SET @WORD7=(parsename(replace(@ProductName,' ','.'),7))



	SET NOCOUNT ON; --bulamadım ama mantıgı burdur heralde
	SELECT
	Products.ProductName,
	Products.ProductId,
	Products.ImageUrl ProductImageUrl,
	Products.ProductDescription
	FROM Products WHERE 
	 
	  (Products.ProductName LIKE '%'+@WORD1+'%' OR @WORD1 IS NULL)
	 AND (Products.ProductName LIKE '%'+@WORD2+'%' OR @WORD2 IS NULL)
	 AND (Products.ProductName LIKE '%'+@WORD3+'%' OR @WORD3 IS NULL)
	 AND (Products.ProductName LIKE '%'+@WORD4+'%' OR @WORD4 IS NULL)
	 AND (Products.ProductName LIKE '%'+@WORD5+'%' OR @WORD5 IS NULL)
	 AND (Products.ProductName LIKE '%'+@WORD6+'%' OR @WORD6 IS NULL)
	 AND (Products.ProductName LIKE '%'+@WORD7+'%' OR @WORD7 IS NULL)
END
GO
