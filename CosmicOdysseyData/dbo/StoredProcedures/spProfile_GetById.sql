CREATE PROCEDURE [dbo].[spProfile_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id], 
		[ObjectIdentifier], 
		[Name], 
		[Bio],
		[ImageUrl], 
		[Email], 
		[DateCreated], 
		[DateUpdated], 
		[HasNotification],
		(SELECT COUNT(*) FROM [dbo].[Following] WHERE [FolloweeId] = @Id) AS 'FollowerCount'
	FROM [dbo].[Profile]
	WHERE [Id] = @Id;
END
