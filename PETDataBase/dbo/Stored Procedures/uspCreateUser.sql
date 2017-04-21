CREATE PROCEDURE [dbo].[uspCreateUser]
  @UserName nvarchar(100),
  @Pass nvarchar(50)
AS
  insert into tbusers ([user_name],pass,creation_date) values (@UserName, @Pass, GETDATE());

  select 
  [USERID],
  [user_name],
  pass,
  cliente_id,
  creation_date,
  expiration_date
  from tbusers
  where
    @username = [user_name] and @pass = [pass]
