USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCoupon]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetCoupon]
	@CouponId int=NULL,
	@CouponStartDate nvarchar(100)=NULL,
	@CouponEndDate nvarchar(100)=NULL,
	@CouponQuantity int=NULL,
	@CouponStatus bit=NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	Select * from Coupons Coupon
	WHERE (Coupon.CouponId=@CouponId OR @CouponId IS NULL)
	AND (Coupon.CouponStartDate=@CouponStartDate OR @CouponStartDate IS NULL)
	AND (Coupon.CouponEndDate=@CouponEndDate OR @CouponEndDate IS NULL)
	AND (Coupon.CouponQuantity=@CouponQuantity OR @CouponQuantity IS NULL)
	AND (Coupon.CouponStatus=@CouponStatus OR @CouponStatus IS NULL)
END
GO
