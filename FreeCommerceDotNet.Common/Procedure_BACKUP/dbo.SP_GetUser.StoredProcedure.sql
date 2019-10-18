USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUser]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetUser]
	@id int = NULL,
	@username nvarchar(150) = NULL,
	@email nvarchar(150) = NULL,
	@password nvarchar(150) = NULL,
	@customerid int = NULL,
	@role nvarchar(150) = NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT 	
	Customer.CustomerId,
	Customer.Firstname,
	Customer.Lastname,
	Customer.Email,
	Customer.Telephone,
	Customer.Password,
	Customer.Address1,
	Customer.Address2,
	Customer.TaxAddress,
	Customer.Status,
	Customer.SegmentId,
	Kullanici.UserId,
	Kullanici.Username,
	Kullanici.Role
	
	FROM Users Kullanici
	LEFT JOIN Customers Customer ON Kullanici.UserId = Customer.UserId 
	WHERE
	
	(Kullanici.UserId =@id OR @id IS NULL)
	AND (Kullanici.Username = @username OR @username IS NULL)
	AND (Kullanici.Password = @password OR @password IS NULL)
	AND (Kullanici.EMail = @email OR @email IS NULL)
	AND (Customer.CustomerId = @customerid OR @customerid IS NULL)
	AND (Kullanici.Role = @role OR @role IS NULL)
END
GO
