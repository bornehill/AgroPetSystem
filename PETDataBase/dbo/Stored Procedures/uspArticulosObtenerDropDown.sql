CREATE PROCEDURE uspArticulosObtenerDropDown
AS
BEGIN
SET NOCOUNT ON;
	Select Id_Art, Nombre
	FROM tbarticulos;
END