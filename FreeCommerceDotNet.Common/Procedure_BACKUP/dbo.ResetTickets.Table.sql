USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[ResetTickets]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResetTickets](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[tokenHash] [uniqueidentifier] NOT NULL,
	[expirationDate] [nvarchar](50) NOT NULL,
	[tokenUsed] [bit] NOT NULL,
 CONSTRAINT [PK_ResetTickets] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResetTickets] ADD  CONSTRAINT [DF_ResetTickets_tokenHash]  DEFAULT (newid()) FOR [tokenHash]
GO
ALTER TABLE [dbo].[ResetTickets] ADD  CONSTRAINT [DF_ResetTickets_expirationDate]  DEFAULT (format(dateadd(day,(1),getdate()),'dd/MM/yyyy','en-us')) FOR [expirationDate]
GO
ALTER TABLE [dbo].[ResetTickets] ADD  CONSTRAINT [DF_ResetTickets_tokenUsed]  DEFAULT ((0)) FOR [tokenUsed]
GO
ALTER TABLE [dbo].[ResetTickets]  WITH CHECK ADD  CONSTRAINT [FK_ResetTickets_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ResetTickets] CHECK CONSTRAINT [FK_ResetTickets_Users]
GO
