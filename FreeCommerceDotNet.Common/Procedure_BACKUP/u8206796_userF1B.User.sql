USE [u8206796_dbF1B]
GO
/****** Object:  User [u8206796_userF1B]    Script Date: 18.10.2019 21:57:51 ******/
CREATE USER [u8206796_userF1B] FOR LOGIN [u8206796_userF1B] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [u8206796_userF1B]
GO
