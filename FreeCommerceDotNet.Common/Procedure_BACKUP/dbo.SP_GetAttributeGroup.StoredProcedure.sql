USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAttributeGroup]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAttributeGroup]
	@AttributeGroupId int=NULL,
	@AttributeGroupName nvarchar(200)=NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM AttributeGroups AttributeGroup
	LEFT JOIN Attributes Attribute ON AttributeGroup.AttributeGroupId=Attribute.AttributeGroup
	WHERE (AttributeGroup.AttributeGroupId=@AttributeGroupId OR @AttributeGroupId IS NULL)
	AND (AttributeGroup.AttributeGroupName=@AttributeGroupName OR @AttributeGroupName IS NULL)
END
GO
