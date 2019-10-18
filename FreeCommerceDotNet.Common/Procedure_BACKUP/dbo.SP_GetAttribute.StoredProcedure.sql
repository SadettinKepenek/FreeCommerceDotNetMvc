USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAttribute]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAttribute]
	@id int = NULL,
	@attibutegroupid int = NULL,
	@attributegroupname nvarchar(200) = NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT 	Attribute.*,
	AttributeGroup.AttributeGroupName
	
	FROM Attributes Attribute
	INNER JOIN AttributeGroups AttributeGroup ON Attribute.AttributeGroup = AttributeGroup.AttributeGroupId

	WHERE
	(Attribute.AttributeId =@id OR @id IS NULL)
	AND	(AttributeGroup.AttributeGroupId = @attibutegroupid OR @attibutegroupid IS NULL)
	AND	(AttributeGroup.AttributeGroupName = @attributegroupname OR @attributegroupname IS NULL)
END
GO
