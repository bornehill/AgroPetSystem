CREATE PROCEDURE [dbo].[usp_GetMenuHijos](
	@PadreMenuId INT)
AS	
BEGIN	
	SELECT       
	H.MenuId      
	,H.Menu      
	,H.MenuUrl    
	,H.Padre
	,H.Nivel    
	,H.Orden    
	,H.Activo      
	,H.FechaCreacion      
	,H.CreacionUsuarioId      
	,H.FechaModificacion      
	,H.ModificacionUsuarioId      
	,P.Menu AS PadreNom    
	FROM tbmenuweb AS H      
	LEFT JOIN tbmenuweb AS P ON H.Padre = P.MenuId    
	WHERE H.Padre = @PadreMenuId
	AND h.activo=1
	order by h.Orden
END