CREATE PROCEDURE [dbo].[spNotification_Insert]
	@ProfileId INT,
    @Body TEXT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Notification] ([ProfileId], [Body])
    VALUES (@ProfileId, @Body);

    SELECT SCOPE_IDENTITY() AS [InsertedId];
END