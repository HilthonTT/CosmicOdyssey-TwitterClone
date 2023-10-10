CREATE PROCEDURE [dbo].[spComment_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Comment]
	WHERE [Id] = @Id;
END