USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductAttribute]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetProductAttribute]
	@id int=NULL,
	@productId int=NULL,
	@attributeId int=NULL
	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM ProductsAttributes ProductAttribute
	INNER JOIN Attributes Attribute ON ProductAttribute.AttributeId=Attribute.AttributeId
	INNER JOIN AttributeGroups AttributeGroup ON Attribute.AttributeGroup=AttributeGroup.AttributeGroupId
	WHERE (ProductAttribute.RelationId = @id OR @id IS NULL)
	AND (ProductAttribute.ProductId=@productId OR @productId IS NULL)
	AND (ProductAttribute.AttributeId=@attributeId OR @attributeId IS NULL)

    
END
GO
