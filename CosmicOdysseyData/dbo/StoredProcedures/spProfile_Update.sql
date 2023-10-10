CREATE PROCEDURE [dbo].[spProfile_Update]
	@Id INT,
	@Name NVARCHAR(128),
	@ImageUrl TEXT,
	@Email NVARCHAR(256),
	@HasNotification BIT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Profile]
	SET [Name] = @Name,
		[ImageUrl] = @ImageUrl,
		[Email] = @Email,
		[HasNotification] = @HasNotification
	WHERE [Id] = @Id;
END