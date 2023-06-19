CREATE TABLE [dbo].[ProductCategory]
(
    [ProductId] INT NOT NULL, 
    [CategoryId] INT NOT NULL, 
    CONSTRAINT [FK_ProductCategory_ToProduct] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]), 
    CONSTRAINT [FK_ProductCategory_ToCategory] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]), 
    CONSTRAINT [PK_ProductCategory] PRIMARY KEY ([ProductId], [CategoryId])
)
