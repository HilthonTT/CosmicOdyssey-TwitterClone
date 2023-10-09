CREATE TABLE [dbo].[Like]
(
	[BlogId] INT,
    [ProfileId] INT,
    CONSTRAINT PK_Likes PRIMARY KEY ([BlogId], [ProfileId]),
    CONSTRAINT [FK_Like_ToBlogId] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blog]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Like_ToProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile]([Id]) ON DELETE NO ACTION
)


GO

CREATE INDEX [IX_Like_BlogId] ON [dbo].[Like] ([BlogId]);

GO

CREATE INDEX [IX_Like_UserId] ON [dbo].[Like] ([ProfileId]);