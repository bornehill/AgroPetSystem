
CREATE PROCEDURE uspSelClientes_Web
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
