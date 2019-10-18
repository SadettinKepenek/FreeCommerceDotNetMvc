USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[TranscationNo] [nvarchar](50) NULL,
	[OrderId] [int] NULL,
	[InvoiceTotalPrice] [numeric](18, 4) NULL,
	[InvoiceStatus] [bit] NULL,
	[InvoiceTotalDiscount] [numeric](18, 4) NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_OrdersMaster] FOREIGN KEY([OrderId])
REFERENCES [dbo].[OrdersMaster] ([OrderId])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_OrdersMaster]
GO
