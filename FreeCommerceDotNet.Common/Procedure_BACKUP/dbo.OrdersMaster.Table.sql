USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[OrdersMaster]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersMaster](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentGatewayId] [int] NULL,
	[ShippingId] [int] NULL,
	[CustomerId] [int] NULL,
	[TrackNumber] [nvarchar](200) NULL,
	[OrderDate] [nvarchar](200) NULL,
	[DeliveryDate] [nvarchar](200) NULL,
	[DeliveryComment] [nvarchar](200) NULL,
	[DeliveryStatus] [nvarchar](200) NULL,
 CONSTRAINT [PK_OrdersMaster] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrdersMaster]  WITH CHECK ADD  CONSTRAINT [FK_OrdersMaster_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[OrdersMaster] CHECK CONSTRAINT [FK_OrdersMaster_Customers]
GO
ALTER TABLE [dbo].[OrdersMaster]  WITH CHECK ADD  CONSTRAINT [FK_OrdersMaster_PaymentGateways] FOREIGN KEY([PaymentGatewayId])
REFERENCES [dbo].[PaymentGateways] ([PaymentId])
GO
ALTER TABLE [dbo].[OrdersMaster] CHECK CONSTRAINT [FK_OrdersMaster_PaymentGateways]
GO
ALTER TABLE [dbo].[OrdersMaster]  WITH CHECK ADD  CONSTRAINT [FK_OrdersMaster_Shippings] FOREIGN KEY([ShippingId])
REFERENCES [dbo].[Shippings] ([ShippingId])
GO
ALTER TABLE [dbo].[OrdersMaster] CHECK CONSTRAINT [FK_OrdersMaster_Shippings]
GO
