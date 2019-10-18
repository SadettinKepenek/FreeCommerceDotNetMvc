USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[GetExpectedDeliveryDate]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetExpectedDeliveryDate]
	@OutputMessage nvarchar(200)=NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @TotalOrderCount int;
	DECLARE @MultiplyBy int;
	SET @TotalOrderCount =(Select COUNT(1) From OrdersMaster OrderMaster WHERE OrderMaster.DeliveryStatus <> 'Teslim Edildi')
	SET @MultiplyBy=CONVERT(DECIMAL(9,2),(@TotalOrderCount/5));
	
	-- Her 5 sipariş için bir gün atacak şekilde otomatik delivery date belirlenir.

	DECLARE @Day nvarchar(250);

	SET @Day =DATENAME(WEEKDAY,FORMAT(DATEADD(d,1,GETDATE()),'dd/MM/yyyy','en-us'))

	if @Day = 'Pazar'
	BEGIN
		set @OutputMessage = FORMAT(DATEADD(d,1+@MultiplyBy,GETDATE()),'dd/MM/yyyy','en-us')
	END
	else if @Day = 'Cumartesi'
	BEGIN
		set @OutputMessage = FORMAT(DATEADD(d,2+@MultiplyBy,GETDATE()),'dd/MM/yyyy','en-us')
	END
	ELSE
	BEGIN
		set @OutputMessage = FORMAT(DATEADD(d,@MultiplyBy,GETDATE()),'dd/MM/yyyy','en-us')
	END

	SELECT @OutputMessage AS ExpectedDate

END
GO
