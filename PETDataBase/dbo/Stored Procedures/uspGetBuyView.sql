CREATE PROCEDURE [dbo].[uspGetBuyView]
  @UserId int
AS
  SELECT gpo.nombre NombreGpoMicrosip, lin.nombre NombreLinMicrosip, art.nombre NombreArticulo, buy.price, buy.lot,
  buy.itemId, imgd.pathimagen Image, buy.userId
  FROM tbTempBuy buy
  INNER JOIN tbarticulos  art
  ON buy.itemId = art.ARTICULO_ID
  INNER JOIN tblineas_articulos  lin
  ON lin.id_lin_art = art.ID_LIN_ART
  INNER JOIN tbgrupos_lineas  gpo
  ON gpo.id_gpo_lin = lin.ID_GRUPO_LIN
  INNER JOIN tbimagenesarticulos img
  ON art.id_art = img.idarticulo
  INNER JOIN tbimagenesarticulosdet imgd
  ON img.idimagenarticulo = imgd.idimagenarticulo
  INNER JOIN tbprecios_articulos pr
  ON art.articulo_id = pr.articulo_id
  INNER JOIN tbprecios_empresa pe
  ON pr.precio_empresa_id = pe.precio_empresa_id
  where buy.userId=@UserId
  and pe.nombre = 'Precio de lista'
  and SUBSTRING(art.Nombre,1,1) <> '*' AND NOT (art.Nombre like '%NO USAR%')
  order by NombreArticulo;

