﻿CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Name] NVARCHAR(150) NOT NULL, 
    [Description] NVARCHAR(150) NOT NULL, 
    [Price] MONEY NOT NULL
)
