CREATE PROCEDURE [dbo].[uspAddBuy]
  @userId int,
  @itemId int,
  @lot decimal(18,2),
  @price decimal(18,2)
AS
  if exists (select 1 from tbTempBuy where userId=@userId and itemId=@itemId)
  begin
    select -1 as UserId, @itemId as itemId, 0.0 as lot, 0.0 as price;
  end
  else
    begin
      insert into tbTempBuy(userId, itemId, lot, price) values (@userId, @itemId, @lot, @price);
      select userId, itemId, lot, price from tbTempBuy where userId=@userId and itemId=@itemId;
    end