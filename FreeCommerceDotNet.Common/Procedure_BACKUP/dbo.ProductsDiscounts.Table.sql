USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[ProductsDiscounts]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsDiscounts](
	[DiscountId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[StartDate] [nvarchar](100) NULL,
	[EndDate] [nvarchar](100) NULL,
	[Quantity] [int] NULL,
	[NewPrice] [numeric](18, 4) NULL,
	[Segment] [int] NULL,
 CONSTRAINT [PK_ProductsDiscounts] PRIMARY KEY CLUSTERED 
(
	[DiscountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductsDiscounts]  WITH CHECK ADD  CONSTRAINT [FK_ProductsDiscounts_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductsDiscounts] CHECK CONSTRAINT [FK_ProductsDiscounts_Products]
GO
ALTER TABLE [dbo].[ProductsDiscounts]  WITH CHECK ADD  CONSTRAINT [FK_ProductsDiscounts_Segments] FOREIGN KEY([Segment])
REFERENCES [dbo].[Segments] ([SegmentId])
GO
ALTER TABLE [dbo].[ProductsDiscounts] CHECK CONSTRAINT [FK_ProductsDiscounts_Segments]
GO
