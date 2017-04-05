CREATE PROCEDURE dbo.uspDetAccesosxPerfil_Seleccion(@nIdPerfil INT)
AS
BEGIN
	SELECT Menu.idmenu, Menu.nombremenu, Menu.menuurl, Menu.ordenh, Menu.idpadre,
		Acc.iddetaccesoxperfil, Acc.tipoacceso, Menu.espadre
	FROM tbmenuadminagropet AS Menu
		INNER JOIN tbdetaccesosxperfil AS Acc
			ON Menu.idmenu = Acc.idmenu
	WHERE Acc.idperfil = @nIdPerfil
	ORDER BY Menu.idpadre, Menu.idmenu, Menu.ordenh;
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspgrupos_Lineas_Seleccion */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
