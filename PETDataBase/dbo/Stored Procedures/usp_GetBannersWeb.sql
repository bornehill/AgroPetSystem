CREATE PROCEDURE dbo.usp_GetBannersWeb(
    @idbanner INT
    , @fechaini DATETIME
    , @fechafin DATETIME)
AS	
BEGIN
    select 
    bd.*
    , b.fechainiaplica as fechaini
    , b.fechafinaplica as fechafin
    from
    tbbannerssitio as b,
    tbbannerssitiodetalle as bd
    where
    (@idbanner=0 or b.idbanner=@idbanner)
    and @fechaini>=b.fechainiaplica
    and @fechafin<=b.fechafinaplica
    and b.idbanner=bd.idbanner
    order by bd.idbanner, bd.orden;
END
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS usp_GetMenuArticulos */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ENGINE_SUBSTITUTION' */ ;
