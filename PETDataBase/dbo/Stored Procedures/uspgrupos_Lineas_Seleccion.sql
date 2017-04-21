
CREATE PROCEDURE uspgrupos_Lineas_Seleccion
AS
BEGIN
	SELECT id_gpo_lin, nombre FROM tbgrupos_lineas;
END ;
