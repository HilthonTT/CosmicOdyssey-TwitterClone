CREATE PROCEDURE [dbo].[spProfile_GetById]
	@Id INT
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
	WHERE [Id] = @Id;;
END