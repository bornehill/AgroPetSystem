
CREATE PROCEDURE uspDetAccesosxPerfil_Seleccion(@nIdPerfil INT)
AS
BEGIN
	SELECT Menu.idmenu, Menu.nombremenu, Menu.menuurl, Menu.ordenh, Menu.idpadre,
		Acc.iddetaccesoxperfil, Acc.tipoacceso, Menu.espadre
	FROM tbmenuadminagropet AS Menu
		INNER JOIN tbdetaccesosxperfil AS Acc
			ON Menu.idmenu = Acc.idmenu
	WHERE Acc.idperfil = @nIdPerfil
	ORDER BY Menu.idpadre, Menu.idmenu, Menu.ordenh;
END ;
