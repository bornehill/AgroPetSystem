CREATE PROCEDURE [dbo].[uspAddBuy]
  @userId int,
  @itemId int,
  @lot decimal(18,2),
  @price decimal(18,2),
	@off decimal(18,2),
	@tax decimal(18,2)
AS
  if exists (select 1 from tbTempBuy where userId=@userId and itemId=@itemId)
  begin
    select -1 as UserId, @itemId as itemId, 0.0 as lot, 0.0 as price, 0.0 as [off], 0.0 as tax;
  end
  else
    begin
      insert into tbTempBuy(userId, itemId, lot, price, [off], tax) values (@userId, @itemId, @lot, @price, @off, @tax);
      select userId, itemId, lot, price, [off], tax from tbTempBuy where userId=@userId and itemId=@itemId;
    end