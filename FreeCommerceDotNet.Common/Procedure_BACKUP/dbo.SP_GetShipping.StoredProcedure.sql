USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetShipping]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetShipping]
	@ShippingId int=NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * from Shippings Shipping
	WHERE (Shipping.ShippingId=@ShippingId OR @ShippingId IS NULL)
END
GO
