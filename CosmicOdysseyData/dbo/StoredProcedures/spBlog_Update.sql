CREATE PROCEDURE [dbo].[spBlog_Update]
	@Id INT,
	@Body TEXT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Blog]
	SET [Body] = @Body,
		[DateUpdated] = GETUTCDATE()
	WHERE [Id] = @Id;
END
