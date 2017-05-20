CREATE PROCEDURE [dbo].[uspUsuariosActualiza]
(
	@idusuario int,
	@idperfil int,
	@claveusr varchar(20),
	@nombreusr varchar(100), 
	@passwordusr varchar(20), 
	@activo bit, 
	@idusuariomodif int
)
AS
BEGIN
    
	Update [dbo].[tbusuarios]
	set idperfil = @idperfil,
		nombreusr = @nombreusr,
		claveusr = @claveusr,
		passwordusr = @passwordusr,
		activo = @activo,
		idusuariomodif = @idusuariomodif,
		fechamodif = GETDATE() 
	where idusuario = @idusuario
	
END