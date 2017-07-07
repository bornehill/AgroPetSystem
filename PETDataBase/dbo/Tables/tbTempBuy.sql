CREATE TABLE [dbo].[tbTempBuy]
(
  [userId] INT NOT NULL , 
    [itemId] INT NOT NULL, 
    [lot] DECIMAL(18, 2) NOT NULL, 
    [price] DECIMAL(18, 2) NOT NULL, 
    [notes] VARCHAR(150) NULL, 
    [off] DECIMAL(18, 2) NULL, 
    [tax] DECIMAL(18, 2) NULL, 
    PRIMARY KEY ([userId], [itemId])
)
