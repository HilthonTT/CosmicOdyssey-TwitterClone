CREATE PROCEDURE [dbo].[spProfile_GetAll]
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
		[DateUpdated]
	FROM [dbo].[Profile]
END