USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPayment]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetPayment]
	@PaymentId int=NULL

AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * from PaymentGateways Payment
	Where (Payment.PaymentId=@PaymentId OR @PaymentId IS NULL)
END
GO
