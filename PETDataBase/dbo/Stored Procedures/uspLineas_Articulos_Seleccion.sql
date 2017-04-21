
CREATE PROCEDURE uspLineas_Articulos_Seleccion(
    @Id_Gpo_Lin INT)
AS
BEGIN
	SELECT id_lin_art, nombre FROM tblineas_articulos WHERE id_grupo_lin = @Id_Gpo_Lin;
END ;
