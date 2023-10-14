CREATE PROCEDURE [dbo].[spProfile_Update]
	@Id INT,
	@Name NVARCHAR(128),
	@Bio TEXT = NULL,
	@ImageUrl TEXT,
	@Email NVARCHAR(256),
	@HasNotification BIT,
	@DateUpdated DATETIME2
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Profile]
	SET [Name] = @Name,
		[Bio] = @Bio,
		[ImageUrl] = @ImageUrl,
		[Email] = @Email,
		[HasNotification] = @HasNotification,
		[DateUpdated] = @DateUpdated
	WHERE [Id] = @Id;
END