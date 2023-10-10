CREATE PROCEDURE [dbo].[spComment_Update]
	@Id INT,
	@Body TEXT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Comment]
	SET [Body] = @Body,
		[DateUpdated] = GETUTCDATE()
	WHERE [Id] = @Id;
END