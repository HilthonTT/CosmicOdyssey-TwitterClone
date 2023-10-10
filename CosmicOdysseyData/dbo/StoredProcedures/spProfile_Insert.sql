CREATE PROCEDURE [dbo].[spProfile_Insert]
	@ObjectIdentifier NVARCHAR(36),
	@Name NVARCHAR(128),
	@ImageUrl TEXT,
	@Email NVARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Profile](
		[ObjectIdentifier],
		[Name],
		[ImageUrl],
		[Email]
	)
	VALUES (
		@ObjectIdentifier,
		@Name,
		@ImageUrl,
		@Email
	)

	SELECT SCOPE_IDENTITY() AS [InsertedId];
END