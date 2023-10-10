CREATE PROCEDURE [dbo].[spNotification_GetByProfileId]
	@ProfileId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id], 
        [ProfileId], 
        [Body],
        [DateCreated]
    FROM [dbo].[Notification]
    WHERE [ProfileId] = @ProfileId;
END