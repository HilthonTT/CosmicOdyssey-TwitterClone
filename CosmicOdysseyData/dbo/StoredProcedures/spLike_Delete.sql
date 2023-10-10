CREATE PROCEDURE [dbo].[spLike_Delete]
	@ProfileId INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Like]
	WHERE [ProfileId] = @ProfileId;
END