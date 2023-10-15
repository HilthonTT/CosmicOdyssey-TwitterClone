﻿CREATE PROCEDURE [dbo].[spProfile_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id],
		[ObjectIdentifier],
		[FirstName],
		[LastName],
		[DisplayName], 
		[Bio], 
		[ProfileImage],
		[CoverImage], 
		[Email],
		[DateCreated],
		[DateUpdated],
		[HasNotification],
		(SELECT COUNT(*) FROM [dbo].[Following] WHERE [FolloweeId] = @Id) AS 'FollowerCount'
	FROM [dbo].[Profile]
	WHERE [Id] = @Id;
END
