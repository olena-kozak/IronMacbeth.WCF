CREATE TABLE [dbo].[Periodical]
(
    [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR (255) NOT NULL,
    [Responsible] VARCHAR (255) NOT NULL,
    [IssueNumber] INT NOT NULL,
    [PublishingHouse] VARCHAR (255) NOT NULL,
    [City] VARCHAR (255) NULL,
    [Year] INT NULL,
    [Pages] INT NOT NULL,
      [Topic] VARCHAR (255) NOT NULL,
    [Availiability] INT NOT NULL,
    [Location] VARCHAR (255) NOT NULL,
    [TypeOfDocument] VARCHAR (255) NOT NULL,
    [ElectronicVersionFileId] UNIQUEIDENTIFIER NULL,
    [RentPrice] VARCHAR (255) NOT NULL,
    [ImageFileId] UNIQUEIDENTIFIER NULL
)
