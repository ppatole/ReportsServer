﻿CREATE TABLE [dbo].[FieldName]
(
	[FieldNameID]  BIGINT NOT NULL PRIMARY KEY IDENTITY , 
    [FieldNameDes] NVARCHAR(200) NULL, 
    [ParentFieldNameID] UNIQUEIDENTIFIER NULL, 
    [ActiveTF] BIT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GetDate(), 
    [UID] UNIQUEIDENTIFIER NULL DEFAULT NEWID()
)
