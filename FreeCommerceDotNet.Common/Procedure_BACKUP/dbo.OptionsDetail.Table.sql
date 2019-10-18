USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[OptionsDetail]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OptionsDetail](
	[ValueId] [int] IDENTITY(1,1) NOT NULL,
	[ValueName] [nvarchar](200) NULL,
	[OptionId] [int] NULL,
 CONSTRAINT [PK_OptionsDetail] PRIMARY KEY CLUSTERED 
(
	[ValueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OptionsDetail]  WITH CHECK ADD  CONSTRAINT [FK_OptionsDetail_OptionsMaster] FOREIGN KEY([OptionId])
REFERENCES [dbo].[OptionsMaster] ([OptionId])
GO
ALTER TABLE [dbo].[OptionsDetail] CHECK CONSTRAINT [FK_OptionsDetail_OptionsMaster]
GO
