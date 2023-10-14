CREATE PROCEDURE [dbo].[spProfile_GetAll]
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
		[HasNotification]
	FROM [dbo].[Profile]
END