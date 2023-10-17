CREATE PROCEDURE [dbo].[spFollowing_GetByFolloweeId]
	@FolloweeId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[FollowerId], 
		[FolloweeId]
	FROM [dbo].[Following]
	WHERE [FolloweeId] = @FolloweeId;
END
