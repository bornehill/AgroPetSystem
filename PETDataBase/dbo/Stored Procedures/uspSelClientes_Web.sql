CREATE PROCEDURE dbo.uspSelClientes_Web
AS
BEGIN
	SELECT
	  userId,
	  NOMBRE,
	  CONTACTO1,
	  ESTATUS,
	  CAUSA_SUSP,
	  LIMITE_CREDITO,
	  MONEDA_ID,
	  COND_PAGO_ID,
	  TIPO_CLIENTE_ID,
	  USUARIO_CREADOR,
	  FECHA_HORA_CREACION,
	  USUARIO_ULT_MODIF,
	  FECHA_HORA_ULT_MODIF,
	   dirsClientesId,
	  clienteId,
	  calle,
	  numero,
	  interior,
	  colonia,
	  estado,
	  email,
	  telefono,
	  celular,
	  codigoPostal,
	  ciudad,
	  pais
	FROM tbclientes_web C
	INNER JOIN tbDirsClientes D
	ON C.userId = D.clienteId
	WHERE C.FECHA_HORA_CREACION between GETDATE()-1 and GETDATE();
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspSeltbTiempoInterfaces */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
