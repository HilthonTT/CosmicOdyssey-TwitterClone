CREATE PROCEDURE [dbo].[spFollowing_Insert]
	@FollowerId INT,
	@FolloweeId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Following] ([FollowerId], [FolloweeId])
	VALUES (@FollowerId, @FolloweeId);
END