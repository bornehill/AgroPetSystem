
CREATE PROCEDURE [dbo].[usp_MenuWebObtener](
	@MenuId INT      
	,@Menu VARCHAR(100))
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
	WHERE ((@MenuId IS NULL AND H.MenuId = H.MenuId) OR (H.MenuId = @MenuId) OR (P.MenuId = @MenuId))
	AND  ((@Menu IS NULL AND H.Menu = H.Menu) OR (H.Menu LIKE '%' + @Menu + '%'))
	AND h.activo=1
	order by h.Orden
END