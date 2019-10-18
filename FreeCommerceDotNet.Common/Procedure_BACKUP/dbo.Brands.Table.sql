USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 18.10.2019 21:57:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](150) NULL,
	[BrandDescription] [nvarchar](500) NULL,
	[BrandUrl] [nvarchar](150) NULL,
	[BrandImageUrl] [nvarchar](150) NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
