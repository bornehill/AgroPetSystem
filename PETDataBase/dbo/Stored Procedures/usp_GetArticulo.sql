CREATE PROCEDURE [dbo].[usp_GetArticulo](@IdArticulo INT)
AS
BEGIN
  select 
	gpo.nombre NombreGpoMicrosip, lin.nombre NombreLinMicrosip, art.nombre NombreArticulo, pr.PRECIO, art.articulo_id IdArticulo, imgd.pathimagen Image 
  from
  tbarticulos  art
  INNER JOIN tblineas_articulos  lin
  ON lin.id_lin_art = art.ID_LIN_ART
  INNER JOIN tbgrupos_lineas  gpo
  ON gpo.id_gpo_lin=lin.ID_GRUPO_LIN
  INNER JOIN tbimagenesarticulos img
  ON art.id_art = img.idarticulo
  INNER JOIN tbprecios_articulos pr
  ON art.articulo_id = pr.articulo_id
  INNER JOIN tbprecios_empresa pe
  ON pr.precio_empresa_id = pe.precio_empresa_id
  INNER JOIN tbimagenesarticulosdet imgd
  ON img.idimagenarticulo = imgd.idimagenarticulo
  where art.articulo_id=@IdArticulo
  and pe.nombre = 'Precio de lista'
  and SUBSTRING(art.Nombre,1,1) <> '*' AND NOT (art.Nombre like '%NO USAR%');
END