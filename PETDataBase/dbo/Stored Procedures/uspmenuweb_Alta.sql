
CREATE PROCEDURE [dbo].[uspmenuweb_Alta](
    @Menu varchar(100),
    @MenuUrl Varchar(250),
    @Padre INT,
    /*IN Nivel INT,
    IN Orden INT,*/
    @Activo BIT,
    /*IN FechaCreacion Datetime,*/
    @CreacionUsuarioId Int)
AS	
BEGIN
	/*SET MenuId = 0;*/
	DECLARE @Nivel INT;
	DECLARE @Orden INT;
	
	select @Orden = dbo.ufn_MenuObtenerOrden(@Padre);
	select @Nivel = dbo.ufn_MenuObtenerNivel(@Padre);
	
	INSERT INTO tbmenuweb
	  (Menu    
	  ,MenuUrl    
	  ,Padre      
	  ,Nivel    
	  ,Orden      
	  ,Activo    
	  ,FechaCreacion    
	  ,CreacionUsuarioId    
	  ,FechaModificacion    
	  ,ModificacionUsuarioId)    
	 VALUES    
	  (@Menu    
	  ,'Mascotas/ArticulosPedido'    
	  ,@Padre      
	  ,@Nivel    
	  ,@Orden      
	  ,@Activo  
	  ,GETDATE()    
	  ,@CreacionUsuarioId    
	  ,NULL    
	  ,NULL);
END ;
