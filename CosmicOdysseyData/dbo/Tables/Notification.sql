CREATE TABLE [dbo].[Notification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProfileId] INT NOT NULL, 
    [Body] TEXT NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_Notification_ToProfile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile]([Id]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_Notification_ProfileId] ON [dbo].[Notification] ([ProfileId])
