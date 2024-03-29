USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[ProductsPrices]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsPrices](
	[PriceId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Price] [numeric](18, 4) NULL,
	[Segment] [int] NULL,
 CONSTRAINT [PK_ProductsPrices] PRIMARY KEY CLUSTERED 
(
	[PriceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductsPrices]  WITH CHECK ADD  CONSTRAINT [FK_ProductsPrices_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductsPrices] CHECK CONSTRAINT [FK_ProductsPrices_Products]
GO
ALTER TABLE [dbo].[ProductsPrices]  WITH CHECK ADD  CONSTRAINT [FK_ProductsPrices_Segments] FOREIGN KEY([Segment])
REFERENCES [dbo].[Segments] ([SegmentId])
GO
ALTER TABLE [dbo].[ProductsPrices] CHECK CONSTRAINT [FK_ProductsPrices_Segments]
GO
