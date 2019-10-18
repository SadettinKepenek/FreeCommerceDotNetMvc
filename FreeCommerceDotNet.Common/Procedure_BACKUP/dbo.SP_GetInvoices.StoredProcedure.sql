USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetInvoices]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetInvoices]
	@InvoiceId int = NULL,
	@OrderId int = NULL,
	@InvoiceTotalPrice numeric(18,4) = NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT 	
	OrderMaster.CustomerId,
	OrderMaster.OrderId,
	Invoice.InvoiceId,
	Invoice.InvoiceStatus,
	Invoice.InvoiceTotalDiscount,
	Invoice.InvoiceTotalPrice,
	Invoice.TranscationNo			
	FROM Invoices Invoice
	INNER JOIN OrdersMaster OrderMaster ON Invoice.OrderId = OrderMaster.OrderId
	WHERE
	(Invoice.InvoiceId = @InvoiceId OR @InvoiceId IS NULL)
	AND	(Invoice.InvoiceTotalPrice = @InvoiceTotalPrice OR @InvoiceTotalPrice IS NULL)

END
GO
