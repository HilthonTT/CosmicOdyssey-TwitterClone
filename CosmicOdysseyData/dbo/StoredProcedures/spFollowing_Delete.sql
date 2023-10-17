CREATE PROCEDURE [dbo].[spFollowing_Delete]
	@FollowerId INT,
	@FolloweeId INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE [dbo].[Following]
	WHERE [FollowerId] = @FollowerId AND [FolloweeId] = @FolloweeId;
END