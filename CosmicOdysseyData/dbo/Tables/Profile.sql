﻿CREATE TABLE [dbo].[Profile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ObjectIdentifier] NVARCHAR(36) UNIQUE NOT NULL, 
    [FirstName] NVARCHAR(128) NOT NULL, 
    [LastName] NVARCHAR(128) NOT NULL, 
    [DisplayName] NVARCHAR(128) NOT NULL, 
    [Bio] TEXT NOT NULL,
    [ProfileImage] TEXT NULL, 
    [CoverImage] TEXT NULL, 
    [Email] NVARCHAR(256) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateUpdated] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
)
