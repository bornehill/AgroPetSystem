CREATE PROCEDURE [dbo].[uspGetBuyView]
  @UserId int
AS
  SELECT '' NombreGpoMicrosip, '' NombreLinMicrosip, '' NombreArticulo, buy.price, buy.lot,
  buy.itemId, '' Image, buy.userId, buy.notes, buy.[off], buy.tax
  FROM tbTempBuy buy
  where buy.userId=@UserId
  order by itemId;