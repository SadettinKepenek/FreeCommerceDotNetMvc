USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStore]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetStore]
AS
BEGIN
	
	SET NOCOUNT ON;
	Select * from Stores Store
END
GO
