USE [u8206796_dbF1B]
GO
/****** Object:  Table [dbo].[Segments]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Segments](
	[SegmentId] [int] IDENTITY(1,1) NOT NULL,
	[Priorty] [nvarchar](150) NULL,
	[SegmentName] [nvarchar](150) NULL,
 CONSTRAINT [PK_Segments] PRIMARY KEY CLUSTERED 
(
	[SegmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
