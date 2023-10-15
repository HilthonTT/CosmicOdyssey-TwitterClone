CREATE PROCEDURE [dbo].[spComment_GetById]
	@Id INT
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
        P.[DisplayName],
        P.[ProfileImage],
        P.[CoverImage]
    FROM [dbo].[Comment] AS C
    INNER JOIN [dbo].[Profile] AS P ON C.[ProfileId] = P.[Id]
    WHERE C.[Id] = @Id;
END