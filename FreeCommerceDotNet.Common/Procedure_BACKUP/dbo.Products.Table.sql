USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NULL,
	[ProductName] [nvarchar](200) NULL,
	[ProductDescription] [nvarchar](500) NULL,
	[MetatagTitle] [nvarchar](150) NULL,
	[MetatagDescription] [nvarchar](500) NULL,
	[MetatagKeywords] [nvarchar](150) NULL,
	[ProductTags] [nvarchar](150) NULL,
	[ProductCode] [nvarchar](150) NULL,
	[ImageUrl] [nvarchar](150) NULL,
	[Image1Url] [nvarchar](150) NULL,
	[Image2Url] [nvarchar](150) NULL,
	[Image3Url] [nvarchar](150) NULL,
	[Image4Url] [nvarchar](150) NULL,
	[SKU] [nvarchar](150) NULL,
	[UPC] [nvarchar](150) NULL,
	[EAN] [nvarchar](150) NULL,
	[JAN] [nvarchar](150) NULL,
	[ISBN] [nvarchar](150) NULL,
	[MPN] [nvarchar](150) NULL,
	[Quantity] [int] NULL,
	[OutOfStockStatus] [nvarchar](150) NULL,
	[AvailableDate] [nvarchar](70) NULL,
	[Rate] [int] NULL,
	[Length] [numeric](18, 4) NULL,
	[Width] [numeric](18, 4) NULL,
	[Height] [numeric](18, 4) NULL,
	[Weight] [numeric](18, 4) NULL,
	[Status] [bit] NULL,
	[Brand] [int] NULL,
	[Rating] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Rate]  DEFAULT ((5)) FOR [Rate]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Brands] FOREIGN KEY([Brand])
REFERENCES [dbo].[Brands] ([BrandId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
