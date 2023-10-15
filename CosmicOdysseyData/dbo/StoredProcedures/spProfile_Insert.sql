CREATE PROCEDURE [dbo].[spProfile_Insert]
	@ObjectIdentifier NVARCHAR(36),
	@FirstName NVARCHAR(128),
	@LastName NVARCHAR(128),
	@DisplayName NVARCHAR(128),
	@Email NVARCHAR(256),
	@Bio TEXT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Profile]([ObjectIdentifier], [FirstName], [LastName], [DisplayName], [Email], [Bio])
	VALUES (@ObjectIdentifier, @FirstName, @LastName, @DisplayName, @Email, @Bio);

	SELECT SCOPE_IDENTITY() AS [InsertedId];
END