CREATE TABLE [dbo].[Blog]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProfileId] INT NOT NULL, 
    [Body] TEXT NOT NULL, 
    [LikeCount] INT NOT NULL DEFAULT 0, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateUpdated] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_Blog_ToProfile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile]([Id]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_Blog_ProfileId] ON [dbo].[Blog] ([ProfileId]);
