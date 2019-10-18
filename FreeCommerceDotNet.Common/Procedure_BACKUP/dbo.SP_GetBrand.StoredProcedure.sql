USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBrand]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetBrand]
	@BrandId int=NULL,
	@BrandName nvarchar(150)=NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	Select * from Brands Brand
	WHERE (Brand.BrandId=@BrandId OR @BrandId IS NULL)
	AND (Brand.BrandName=@BrandName OR @BrandName IS NULL)
END
GO
