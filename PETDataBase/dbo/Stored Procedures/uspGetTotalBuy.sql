CREATE PROCEDURE [dbo].[uspGetTotalBuy]
  @UserId int
AS
  SELECT sum(buy.lot) lot, sum(buy.price*buy.lot) price, buy.userId, 0 ItemId
  FROM tbTempBuy buy
  where buy.userId=@UserId
  group by buy.userId;