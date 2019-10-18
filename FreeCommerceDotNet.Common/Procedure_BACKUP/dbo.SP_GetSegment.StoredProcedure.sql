USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSegment]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetSegment]
	@SegmentId int=NULL,
	@SegmentName nvarchar(150) = NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	Select * from Segments Segment
	WHERE (Segment.SegmentId=@SegmentId OR @SegmentId IS NULL)
	AND (Segment.SegmentName=@SegmentName OR @SegmentName IS NULL)
END
GO
