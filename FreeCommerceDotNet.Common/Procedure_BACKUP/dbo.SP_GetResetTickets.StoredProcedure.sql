USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetResetTickets]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetResetTickets]
	@TicketId int = NULL,
	@UserId int = NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT 	ResetTicket.*
	
	FROM ResetTickets ResetTicket
	WHERE
	(ResetTicket.TicketId =@TicketId OR @TicketId IS NULL)
	AND	(ResetTicket.UserId = @UserId OR @UserId IS NULL)

END
GO
