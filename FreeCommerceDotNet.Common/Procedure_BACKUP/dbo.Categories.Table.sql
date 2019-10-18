USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 18.10.2019 21:57:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[CategoryName] [nvarchar](300) NULL,
	[Description] [nvarchar](2000) NULL,
	[MetatagTitle] [nvarchar](300) NULL,
	[MetatagDescription] [nvarchar](2000) NULL,
	[Metatagkeywords] [nvarchar](300) NULL,
	[ImageUrl] [nvarchar](300) NULL,
	[ShowNavbar] [bit] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_ParentId]  DEFAULT ((-1)) FOR [ParentId]
GO
