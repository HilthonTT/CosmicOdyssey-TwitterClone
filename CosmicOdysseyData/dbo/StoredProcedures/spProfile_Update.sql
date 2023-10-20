CREATE PROCEDURE [dbo].[spProfile_Update]
	@Id INT,
	@FirstName NVARCHAR(128),
	@LastName NVARCHAR(128),
	@DisplayName NVARCHAR(128),
	@Bio TEXT,
	@ProfileImage TEXT,
	@CoverImage TEXT,
	@Email NVARCHAR(256),
	@DateUpdated DATETIME2
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Profile]
	SET [FirstName] = @FirstName,
		[LastName] = @LastName,
		[DisplayName] = @DisplayName,
		[Bio] = @Bio,
		[ProfileImage] = @ProfileImage,
		[CoverImage] = @CoverImage,
		[Email] = @Email,
		[DateUpdated] = @DateUpdated
	WHERE [Id] = @Id;
END