USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[ProductsAttributes]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsAttributes](
	[RelationId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[AttributeId] [int] NULL,
	[AttributeDescription] [nvarchar](150) NULL,
 CONSTRAINT [PK_ProductsAttributes] PRIMARY KEY CLUSTERED 
(
	[RelationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductsAttributes]  WITH CHECK ADD  CONSTRAINT [FK_ProductsAttributes_Attributes] FOREIGN KEY([AttributeId])
REFERENCES [dbo].[Attributes] ([AttributeId])
GO
ALTER TABLE [dbo].[ProductsAttributes] CHECK CONSTRAINT [FK_ProductsAttributes_Attributes]
GO
ALTER TABLE [dbo].[ProductsAttributes]  WITH CHECK ADD  CONSTRAINT [FK_ProductsAttributes_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductsAttributes] CHECK CONSTRAINT [FK_ProductsAttributes_Products]
GO
