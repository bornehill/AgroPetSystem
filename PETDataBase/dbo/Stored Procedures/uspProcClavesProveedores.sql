


CREATE PROCEDURE uspProcClavesProveedores(@cveprovID INT, @cveProv VARCHAR(20), @proveedorID INT, @rolCveProvID INT)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tbclaves_proveedores
		WHERE CLAVE_PROV_ID = @cveprovID AND PROVEEDOR_ID = @proveedorID)
	BEGIN
		INSERT INTO tbclaves_proveedores
		(
		CLAVE_PROV_ID,
		CLAVE_PROV,
		PROVEEDOR_ID,
		ROL_CLAVE_PROV_ID)
		VALUES
		(
		@cveprovID,
		@cveProv,
		@proveedorID,
		@rolCveProvID);
    END;
END ;
