
CREATE PROCEDURE usptbMarca_Alta(
    @Id_Marca INT,
    @Descripcion VARCHAR(100),
    @UsuarioCreacion VARCHAR(31),
    @FechaCreacion Datetime,
    @Ret int OUT,
    @MsgProceso VARCHAR(100) OUT)
AS	
BEGIN
	INSERT INTO tbmarcas 
		(Id_Marca, 
		Descripcion, 
		UsuarioCreacion, 
		FechaCreacion
		)
		VALUES
		(@Id_Marca, 
		@Descripcion, 
		@UsuarioCreacion, 
		@FechaCreacion
		);
	
	SET @Ret = @@ROWCOUNT;
	SET @MsgProceso = '';
END
