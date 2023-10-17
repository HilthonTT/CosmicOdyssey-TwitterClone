CREATE PROCEDURE [dbo].[spFollowing_GetByFollowerId]
	@FollowerId INT
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT *
	FROM [dbo].[Following]
	WHERE [FollowerId] = @FollowerId;
END
