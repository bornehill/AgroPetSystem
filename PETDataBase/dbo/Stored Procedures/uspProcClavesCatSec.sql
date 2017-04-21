


CREATE PROCEDURE uspProcClavesCatSec(@nombreTabla VARCHAR(31), @elemID INT, @cve VARCHAR(20))
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tbClaves_cat_sec
		WHERE Nombre_Tabla = @nombreTabla AND Elem_ID = @elemID)
	BEGIN
		INSERT INTO tbclaves_cat_sec
		(NOMBRE_TABLA,
		ELEM_ID,
		CLAVE)
		VALUES
		(@nombreTabla,
		@elemID,
		@cve);
    END
END ;
