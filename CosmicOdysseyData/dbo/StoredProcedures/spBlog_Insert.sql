CREATE PROCEDURE [dbo].[spBlog_Insert]
	@ProfileId INT,
	@Body TEXT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Blog] ([ProfileId], [Body], [DateUpdated])
	VALUES (@ProfileId, @Body, GETUTCDATE());

	SELECT SCOPE_IDENTITY() AS [InsertedId];
END
