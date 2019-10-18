USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[Shippings]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shippings](
	[ShippingId] [int] IDENTITY(1,1) NOT NULL,
	[ShippingName] [nvarchar](150) NULL,
	[ShippingDescription] [nvarchar](500) NULL,
	[ShippingRate] [numeric](18, 4) NULL,
 CONSTRAINT [PK_Shippings] PRIMARY KEY CLUSTERED 
(
	[ShippingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
