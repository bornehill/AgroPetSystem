CREATE TABLE [dbo].[tbTempBuy]
(
  [userId] INT NOT NULL , 
    [itemId] INT NOT NULL, 
    [lot] DECIMAL(18, 2) NOT NULL, 
    [price] DECIMAL(18, 2) NOT NULL, 
    PRIMARY KEY ([userId], [itemId])
)
