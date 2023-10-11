CREATE PROCEDURE [dbo].[spProfile_Insert]
	@ObjectIdentifier NVARCHAR(36),
	@Name NVARCHAR(128),
	@Email NVARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Profile]([ObjectIdentifier], [Name], [Email])
	VALUES (@ObjectIdentifier, @Name, @Email);

	SELECT SCOPE_IDENTITY() AS [InsertedId];
END