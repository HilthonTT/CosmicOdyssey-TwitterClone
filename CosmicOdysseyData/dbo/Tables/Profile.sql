﻿CREATE TABLE [dbo].[Profile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ObjectIdentifier] NVARCHAR(36) NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL, 
    [ImageUrl] TEXT NOT NULL, 
    [Email] NVARCHAR(256) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateUpdated] DATETIME2 NOT NULL, 
    [HasNotification] BIT NOT NULL,
)
