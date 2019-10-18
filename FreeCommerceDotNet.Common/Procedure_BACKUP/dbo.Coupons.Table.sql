USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[Coupons]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupons](
	[CouponId] [int] IDENTITY(1,1) NOT NULL,
	[CouponStartDate] [nvarchar](100) NULL,
	[CouponEndDate] [nvarchar](100) NULL,
	[CouponDiscount] [nvarchar](50) NULL,
	[CouponQuantity] [int] NULL,
	[CouponStatus] [bit] NULL,
 CONSTRAINT [PK_Coupons] PRIMARY KEY CLUSTERED 
(
	[CouponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
