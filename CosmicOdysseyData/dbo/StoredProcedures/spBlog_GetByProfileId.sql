CREATE PROCEDURE [dbo].[spBlog_GetByProfileId]
	@ProfileId INT
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
        P.[DisplayName],
        P.[ProfileImage],
        P.[CoverImage]
    FROM [dbo].[Blog] AS B
    INNER JOIN [dbo].[Profile] AS P ON B.[ProfileId] = P.[Id]
    WHERE B.[ProfileId] = @ProfileId
    ORDER BY B.[DateCreated] DESC;

    -- Retrieve liked profiles for each blog
    SELECT [BlogId], [ProfileId]
    FROM [dbo].[Like]
END
