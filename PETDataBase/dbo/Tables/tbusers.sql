CREATE TABLE [dbo].[tbusers]
(
  [userId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [user_name] NVARCHAR(100) NOT NULL, 
    [pass] NVARCHAR(50) NOT NULL, 
    [cliente_id] INT NULL, 
    [creation_date] DATETIME NOT NULL, 
    [expiration_date] DATETIME NULL
)
