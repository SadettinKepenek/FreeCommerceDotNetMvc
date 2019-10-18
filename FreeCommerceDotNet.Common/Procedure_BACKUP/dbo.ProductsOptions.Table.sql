USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[ProductsOptions]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsOptions](
	[RelationId] [int] IDENTITY(1,1) NOT NULL,
	[ValueId] [int] NULL,
	[Quantity] [int] NULL,
	[AdditionalPrice] [numeric](18, 4) NULL,
	[AdditionalWeight] [numeric](18, 4) NULL,
	[ProductId] [int] NULL,
 CONSTRAINT [PK_ProductsOptions] PRIMARY KEY CLUSTERED 
(
	[RelationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductsOptions]  WITH CHECK ADD  CONSTRAINT [FK_ProductsOptions_OptionsDetail] FOREIGN KEY([ValueId])
REFERENCES [dbo].[OptionsDetail] ([ValueId])
GO
ALTER TABLE [dbo].[ProductsOptions] CHECK CONSTRAINT [FK_ProductsOptions_OptionsDetail]
GO
ALTER TABLE [dbo].[ProductsOptions]  WITH CHECK ADD  CONSTRAINT [FK_ProductsOptions_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductsOptions] CHECK CONSTRAINT [FK_ProductsOptions_Products]
GO
