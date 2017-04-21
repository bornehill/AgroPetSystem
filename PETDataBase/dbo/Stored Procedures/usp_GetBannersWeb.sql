
CREATE PROCEDURE usp_GetBannersWeb(
    @idbanner INT
    , @fechaini DATETIME
    , @fechafin DATETIME)
AS	
BEGIN
    select 
    bd.*
    , b.fechainiaplica as fechaini
    , b.fechafinaplica as fechafin
    from
    tbbannerssitio as b,
    tbbannerssitiodetalle as bd
    where
    (@idbanner=0 or b.idbanner=@idbanner)
    and @fechaini>=b.fechainiaplica
    and @fechafin<=b.fechafinaplica
    and b.idbanner=bd.idbanner
    order by bd.idbanner, bd.orden;
END
