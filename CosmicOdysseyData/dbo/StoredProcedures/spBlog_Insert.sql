CREATE PROCEDURE [dbo].[spBlog_Insert]
	@ProfileId INT,
	@Body TEXT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Blog] ([ProfileId], [Body])
	VALUES (@ProfileId, @Body);

	SELECT SCOPE_IDENTITY() AS [InsertedId];
END
