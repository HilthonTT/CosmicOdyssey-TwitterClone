CREATE PROCEDURE [dbo].[spLike_GetByBlogId]
	@BlogId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[BlogId],
		[ProfileId]
	FROM [dbo].[Like]
	WHERE [BlogId] = @BlogId;
END