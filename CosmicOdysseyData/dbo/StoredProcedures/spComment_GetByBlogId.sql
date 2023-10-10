CREATE PROCEDURE [dbo].[spComment_GetByBlogId]
	@BlogId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
        C.[Id] AS [Id], 
        C.[ProfileId], 
        C.[BlogId],
        C.[Body], 
        C.[DateCreated], 
        C.[DateUpdated],
        P.[Id] AS [Id],
        P.[Name],
        P.[ImageUrl]
    FROM [dbo].[Comment] AS C
    INNER JOIN [dbo].[Profile] AS P ON C.[ProfileId] = P.[Id]
    WHERE C.[BlogId] = @BlogId;
END