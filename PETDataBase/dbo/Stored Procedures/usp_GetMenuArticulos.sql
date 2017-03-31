CREATE PROCEDURE dbo.usp_GetMenuArticulos(@MenuId INT)
AS
BEGIN
	SELECT gpo.nombre NombreGpoMicrosip, lin.nombre NombreLinMicrosip, art.nombre NombreArticulo, art.articulo_id IdArticulo, imgd.pathimagen Image
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
  INNER JOIN tbimagenesarticulosdet imgd
  ON img.idimagenarticulo = imgd.idimagenarticulo    
	WHERE menu.MenuId = @MenuId;
END
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS usp_MenuWebObtener */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ENGINE_SUBSTITUTION' */ ;
