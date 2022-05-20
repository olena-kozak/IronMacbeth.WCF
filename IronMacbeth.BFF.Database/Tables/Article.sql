CREATE TABLE [dbo].[Article]
(
    [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR (255) NOT NULL,
    [Author] VARCHAR (255) NOT NULL,
    [Year] INT NULL,
    [Pages] INT NOT NULL,
    [TypeOfDocument] VARCHAR (255) NOT NULL,
    [MainDocumentId] VARCHAR (255) NULL,
      [Topic] VARCHAR (255) NOT NULL,
    [ElectronicVersionFileId] UNIQUEIDENTIFIER NULL,
  
  )
