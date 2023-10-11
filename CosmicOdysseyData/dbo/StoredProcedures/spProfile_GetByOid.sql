CREATE PROCEDURE [dbo].[spProfile_GetByOid]
	@ObjectIdentifier NVARCHAR(36)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		[Id],
		[ObjectIdentifier], 
		[Name], 
		[ImageUrl], 
		[Email], 
		[DateCreated], 
		[DateUpdated], 
		[HasNotification]
	FROM [dbo].[Profile]
	WHERE [ObjectIdentifier] = @ObjectIdentifier;
END