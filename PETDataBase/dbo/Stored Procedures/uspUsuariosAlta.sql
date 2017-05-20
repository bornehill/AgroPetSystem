CREATE PROCEDURE [dbo].[uspUsuariosAlta]
(
	@idperfil int,
	@claveusr varchar(20),
	@nombreusr varchar(100), 
	@passwordusr varchar(20), 
	@activo bit, 
	@idusuariocreo int
)
AS
BEGIN
    
	IF NOT EXISTS(SELECT * FROM [dbo].[tbusuarios] WHERE claveusr = @claveusr)
	begin
		INSERT INTO [dbo].[tbusuarios](idperfil, nombreusr, claveusr, passwordusr, activo, idusuariocreo, fechacreacion)
		VALUES (@idperfil, @nombreusr, @claveusr, @passwordusr, @activo, @idusuariocreo, GETDATE())
    end   
	 
END