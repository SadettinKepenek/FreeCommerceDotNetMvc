USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomer]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetCustomer]
@id int=NULL,
	@username nvarchar(150)=NULL,
	@userid int = NULL,
	@segmentid int=NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT  
	Customer.CustomerId CustomerId,
	Customer.Firstname CustomerFirstName,
	Customer.Lastname CustomerLastName,
	Customer.Email CustomerEmail,
	Customer.Telephone CustomerTelephone,
    Customer.Address1 CustomerAddress1,
	Customer.Address2 CustomerAddress2,
	       Customer.TaxAddress CustomerTaxAddress,
	Segment.SegmentId CustomerSegmentId,
	Segment.SegmentName CustomerSegmentName,
	Kullanici.UserId  CustomerUserId,
	Kullanici.Username CustomerUserName,
	Kullanici.Password CustomerKullaniciPassword,
	Kullanici.EMail CustomerUserEmail

	
	FROM Customers Customer
	INNER JOIN Segments Segment ON Customer.SegmentId=Segment.SegmentId
	INNER JOIN Users Kullanici ON Customer.UserId = Kullanici.UserId
	WHERE (Customer.CustomerId=@id OR @id IS NULL)
	
	AND (Kullanici.Username = @username OR @username IS NULL)
	AND (Segment.SegmentId = @segmentid OR @segmentid IS NULL)
	AND (Kullanici.UserId = @userid OR @userid IS NULL)
END
GO
