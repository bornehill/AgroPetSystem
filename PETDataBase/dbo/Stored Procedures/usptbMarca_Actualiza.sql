
CREATE PROCEDURE usptbMarca_Actualiza(
    @Id_Marca INT,
    @Descripcion VARCHAR(100),
    @UsuarioCreacion VARCHAR(31),
    @FechaCreacion DATETIME)
AS	
BEGIN
	UPDATE tbmarcas
		SET
		Descripcion = @Descripcion, 
		UsuarioCreacion = @UsuarioCreacion, 
		FechaCreacion = @FechaCreacion		
		WHERE
		Id_Marca = @Id_Marca;
END
