﻿CREATE PROCEDURE [dbo].[spBlog_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        B.[Id], 
        B.[ProfileId],
        B.[Body],
        B.[DateCreated],
        B.[DateUpdated],
        P.[Id] AS [Id],
        P.[Name],
        P.[ImageUrl]
    FROM [dbo].[Blog] AS B
    INNER JOIN [dbo].[Profile] AS P ON B.[ProfileId] = P.[Id];
END