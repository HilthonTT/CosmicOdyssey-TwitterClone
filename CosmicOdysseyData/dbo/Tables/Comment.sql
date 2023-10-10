CREATE TABLE [dbo].[Comment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProfileId] INT NOT NULL, 
    [BlogId] INT NOT NULL, 
    [Body] TEXT NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateUpdated] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_Comment_ToBlog] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blog]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comment_ToProfile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile]([Id]) ON DELETE NO ACTION, 
)


GO

CREATE INDEX [IX_Comment_BlogId] ON [dbo].[Comment]([BlogId]);

GO

CREATE INDEX [IX_Comment_ProfileId] ON [dbo].[Comment]([ProfileId]);
