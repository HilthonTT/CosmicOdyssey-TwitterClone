CREATE TABLE [dbo].[Following]
(
	[FollowerId] INT,
    [FolloweeId] INT,
    CONSTRAINT PK_Following PRIMARY KEY ([FollowerId], [FolloweeId]),
    CONSTRAINT FK_Follower FOREIGN KEY ([FollowerId]) REFERENCES [dbo].[Profile]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_Followee FOREIGN KEY ([FolloweeId]) REFERENCES [dbo].[Profile]([Id]) ON DELETE NO ACTION
)


GO

CREATE INDEX IX_Following_FolloweeId ON [dbo].[Following] (FolloweeId);

GO

CREATE INDEX IX_Following_FollowerId ON [dbo].[Following] (FollowerId);