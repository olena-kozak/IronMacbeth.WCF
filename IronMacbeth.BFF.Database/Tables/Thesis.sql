CREATE TABLE [dbo].[Thesis]
(
    [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR (255) NOT NULL,
    [Author] VARCHAR (255) NOT NULL,
    [Responsible] VARCHAR (255) NULL,
    [City] VARCHAR (255) NULL,
    [Year] INT NULL,
    [Pages] INT NOT NULL,
      [Topic] VARCHAR (255) NOT NULL,
    [TypeOfDocument] VARCHAR (255) NOT NULL,
    [ElectronicVersionFileId] UNIQUEIDENTIFIER NULL,
   )
