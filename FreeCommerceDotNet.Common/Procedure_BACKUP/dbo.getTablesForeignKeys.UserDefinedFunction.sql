USE [u8206796_dbF1B]
GO
/****** Object:  UserDefinedFunction [dbo].[getTablesForeignKeys]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[getTablesForeignKeys] (
    @tableName nvarchar(500)
)
RETURNS TABLE
AS
RETURN
   SELECT 
   OBJECT_NAME(f.parent_object_id) TableName,
   COL_NAME(fc.parent_object_id,fc.parent_column_id) ColName
		FROM 
   sys.foreign_keys AS f
INNER JOIN 
   sys.foreign_key_columns AS fc 
      ON f.OBJECT_ID = fc.constraint_object_id
INNER JOIN 
   sys.tables t 
      ON t.OBJECT_ID = fc.referenced_object_id
WHERE 
   OBJECT_NAME (f.referenced_object_id) = @tableName
GO
