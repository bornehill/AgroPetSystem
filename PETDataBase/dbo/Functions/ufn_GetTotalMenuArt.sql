CREATE FUNCTION [dbo].[ufn_GetTotalMenuArt]
(
  @MenuId int
)
RETURNS int
  Begin
    RETURN 
      (
        SELECT count(art.articulo_id)
        FROM tbRelMenuArticulos  rel
        INNER JOIN tbgrupos_lineas  gpo
        ON gpo.id_gpo_lin = rel.IdGrupo_Linea
        INNER JOIN tblineas_articulos  lin
        ON lin.id_lin_art = rel.IdLinea_Articulo
        INNER JOIN tbarticulos  art
        ON art.id_art = rel.IdArticulo
        INNER JOIN tbmenuweb menu
        ON menu.MenuId = rel.MenuId
        INNER JOIN tbimagenesarticulos img
        ON art.id_art = img.idarticulo
        INNER JOIN tbprecios_articulos pr
        ON art.articulo_id = pr.articulo_id
        INNER JOIN tbprecios_empresa pe
        ON pr.precio_empresa_id = pe.precio_empresa_id
        INNER JOIN tbimagenesarticulosdet imgd
        ON img.idimagenarticulo = imgd.idimagenarticulo    
        WHERE menu.MenuId = @MenuId
        and pe.nombre = 'Precio de lista'
        and SUBSTRING(art.Nombre,1,1) <> '*' AND NOT (art.Nombre like '%NO USAR%')
      )
    END;
