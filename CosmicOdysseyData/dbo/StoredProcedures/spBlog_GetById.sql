CREATE PROCEDURE [dbo].[spBlog_GetById]
	@Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        B.[Id], 
        B.[ProfileId],
        B.[Body],
        B.[LikeCount],
        B.[DateCreated],
        B.[DateUpdated],
        P.[Id] AS [Id],
        P.[ObjectIdentifier],
        P.[FirstName],
        P.[LastName],
        P.[DisplayName],
        P.[Bio],
        P.[ProfileImage],
        P.[CoverImage],
        P.[Email],
        P.[DateCreated] AS [ProfileDateCreated],
        P.[DateUpdated] AS [ProfileDateUpdated]
    FROM [dbo].[Blog] AS B
    INNER JOIN [dbo].[Profile] AS P ON B.[ProfileId] = P.[Id]
    WHERE B.[Id] = @Id
    ORDER BY B.[DateCreated] DESC;

    -- Retrieve liked profiles for each blog
    SELECT [BlogId], [ProfileId]
    FROM [dbo].[Like];
END