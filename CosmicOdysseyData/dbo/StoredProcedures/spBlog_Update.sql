CREATE PROCEDURE [dbo].[spBlog_Update]
	@Id INT,
	@Body TEXT,
	@LikeCount INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Blog]
	SET [Body] = @Body,
		[LikeCount] = ISNULL(@LikeCount, [LikeCount]),
		[DateUpdated] = GETUTCDATE()
	WHERE [Id] = @Id;
END