CREATE PROCEDURE [dbo].[spNotification_GetById]
	@Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id], 
        [ProfileId],
        [Body],
        [DateCreated]
    FROM [dbo].[Notification]
    WHERE [Id] = @Id;
END