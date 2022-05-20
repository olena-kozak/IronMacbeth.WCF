CREATE TABLE [dbo].[Book]
(
    [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR (255) NOT NULL,
    [Author] VARCHAR (255) NOT NULL,
    [PublishingHouse] VARCHAR (255) NULL,
    [City] VARCHAR (255) NULL,
    [Year] INT NULL,
    [Pages] INT NOT NULL,
    [TypeOfDocument] VARCHAR (255) NOT NULL,
    [Topic] VARCHAR (255) NOT NULL,
    [Availiability] INT NOT NULL,
    [Location] VARCHAR (255) NOT NULL,
    [ElectronicVersionFileId] UNIQUEIDENTIFIER NULL,
    [ImageFileId] UNIQUEIDENTIFIER NULL,
    [RentPrice] VARCHAR (255) NULL   
)
