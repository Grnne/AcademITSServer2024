-- USE master;
-- ALTER DATABASE [TestShop] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
-- DROP DATABASE [TestShop];
-- GO

CREATE DATABASE [TestShop];

GO

USE [TestShop];

GO

CREATE TABLE [dbo].[Category]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL
);

INSERT [dbo].[Category]([Name]) 
VALUES (N'Fish'), (N'Meat'), (N'Dairy'), (N'Poultry');

CREATE TABLE [dbo].[Product]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
	[Price] DECIMAL NOT NULL,
	[CategoryId] INT REFERENCES [dbo].[Category]([Id])
);

INSERT [dbo].[Product]([Name], [Price], [CategoryId]) 
VALUES (N'Tuna', 666, 1), (N'Salmon', 500, 1), (N'Chicken', 300, 4);

GO