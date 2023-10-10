CREATE PROCEDURE [dbo].[spComment_Insert]
	@ProfileId INT,
	@BlogId INT,
	@Body TEXT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Comment]([ProfileId], [BlogId], [Body])
	VALUES (@ProfileId, @BlogId, @Body);

	SELECT SCOPE_IDENTITY() AS [InsertedId];
END