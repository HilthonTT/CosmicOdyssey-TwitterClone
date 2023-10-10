CREATE PROCEDURE [dbo].[spBlog_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Blog]
	WHERE [Id] = @Id;
END