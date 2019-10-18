USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[OrdersReturns]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersReturns](
	[ReturnId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[ProductId] [int] NULL,
	[BoxOpened] [bit] NULL,
	[ReturnStatus] [bit] NULL,
	[ReturnReason] [nvarchar](500) NULL,
	[Comment] [nvarchar](500) NULL,
	[ReturnResponse] [nvarchar](500) NULL,
 CONSTRAINT [PK_OrdersReturns] PRIMARY KEY CLUSTERED 
(
	[ReturnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrdersReturns]  WITH CHECK ADD  CONSTRAINT [FK_OrdersReturns_OrdersMaster] FOREIGN KEY([OrderId])
REFERENCES [dbo].[OrdersMaster] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrdersReturns] CHECK CONSTRAINT [FK_OrdersReturns_OrdersMaster]
GO
ALTER TABLE [dbo].[OrdersReturns]  WITH CHECK ADD  CONSTRAINT [FK_OrdersReturns_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrdersReturns] CHECK CONSTRAINT [FK_OrdersReturns_Products]
GO
