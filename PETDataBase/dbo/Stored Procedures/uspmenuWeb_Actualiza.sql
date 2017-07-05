
CREATE PROCEDURE [dbo].[uspmenuWeb_Actualiza](
	@Id INT    
	,@Menu varchar(100)    
	,@MenuUrl varchar(250)    
	,@Padre int    
	/*,IN Nivel int    
	,in Orden int*/    
	,@Activo bit    
	/*,IN FechaCreacion datetime    
	,IN CreacionUsuarioId int*/    
	/*,IN FechaModificacion datetime*/    
	,@ModificacionUsuarioId int )
AS	
BEGIN
	DECLARE @Nivel INT;
	DECLARE @Orden INT;
	
	--select @Orden=dbo.ufn_MenuObtenerOrden(@Padre);
	--select @Nivel=dbo.ufn_MenuObtenerNivel(@Padre);

	if(@Id = @Padre)
		set @Padre = 0

	UPDATE tbmenuweb   
	SET Menu = @Menu    
	,MenuUrl = 'Mascotas/ArticulosPedido'    
	,Padre = @Padre    
	--,Nivel = @Nivel    
	--,Orden = @Orden    
	,Activo = @Activo    
	,FechaModificacion = GETDATE()
	,ModificacionUsuarioId = @ModificacionUsuarioId    
	WHERE MenuId = @Id;
END ;
