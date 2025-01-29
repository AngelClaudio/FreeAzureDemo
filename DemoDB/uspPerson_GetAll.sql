CREATE PROCEDURE [dbo].[uspPerson_GetAll]

AS
BEGIN
	SELECT [Id], [FirstName], [LastName] FROM Person;
END
