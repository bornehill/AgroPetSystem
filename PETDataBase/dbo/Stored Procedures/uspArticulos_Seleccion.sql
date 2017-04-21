
CREATE PROCEDURE uspArticulos_Seleccion(
    @Id_Linea_Art int)
AS	
BEGIN
	select id_art, nombre from tbarticulos where id_lin_art = @Id_Linea_Art;
END ;
