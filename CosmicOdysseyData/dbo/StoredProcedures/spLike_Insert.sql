CREATE PROCEDURE [dbo].[spLike_Insert]
	@BlogId INT,
	@ProfileId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Like] ([BlogId], [ProfileId])
	VALUES (@BlogId, @ProfileId);
END
