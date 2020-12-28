CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(100) NOT NULL, 
    [color] NVARCHAR(100) NOT NULL, 
    [description] NVARCHAR(100) NOT NULL, 
    [price] INT NOT NULL, 
    [quantity] INT NOT NULL
)
