USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stores](
	[StoreId] [int] IDENTITY(1,1) NOT NULL,
	[MetaTitle] [nvarchar](200) NULL,
	[MetaTagDescription] [nvarchar](1000) NULL,
	[MetaTagKeywords] [nvarchar](500) NULL,
	[StoreName] [nvarchar](200) NULL,
	[StoreOwner] [nvarchar](200) NULL,
	[Address] [nvarchar](400) NULL,
	[EMail] [nvarchar](200) NULL,
	[Phone] [nvarchar](100) NULL,
	[CellPhone] [nvarchar](100) NULL,
	[Fax] [nvarchar](100) NULL,
	[ImageUrl] [nvarchar](300) NULL,
	[OpeningTimes] [nvarchar](300) NULL,
	[Comment] [nvarchar](300) NULL,
	[AllowReviews] [bit] NULL,
	[DisplayPricesWithTax] [bit] NULL,
	[LoginDisplayPrices] [bit] NULL,
	[MaxLoginAttempts] [int] NULL,
	[DisplayStock] [bit] NULL,
	[ShowOutOfStockWarning] [bit] NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
