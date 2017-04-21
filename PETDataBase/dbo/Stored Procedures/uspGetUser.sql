CREATE PROCEDURE [dbo].[uspGetUser]
  @userid int = 0,
  @username varchar(100),
  @pass varchar(50)
AS
select 
[USERID],
[user_name],
pass,
cliente_id,
creation_date,
expiration_date
from tbusers
where
(@userid=0 or @userid = userid)
and ((@username is null and @pass is null) 
      or (@username = [user_name] and @pass = [pass])
      or (@username = [user_name] and @pass is null)
     )
