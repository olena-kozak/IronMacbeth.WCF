CREATE TABLE [dbo].[Order]
(
    [Id] INT  NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [UserLogin] VARCHAR (255) NOT NULL,
    [BookId] INT NULL,
    [ArticleId] INT NULL,
    [PeriodicalId] INT NULL,
    [NewspaperId] INT NULL,
    [ThesesId] INT NULL,
    [DateOfOrder] DATETIME2 NOT NULL,
    [DateOfReturn] DATETIME2 NULL,
    [ReceiveDate] DATETIME2 NOT NULL,
    [StatusOfOrder] VARCHAR (255) NOT NULL,
    [UserName] VARCHAR (255) NOT NULL,
    [UserSurname] VARCHAR (255) NOT NULL,
    [PhoneNumber] INT NOT NULL,
    [TypeOfOrder] VARCHAR (255) NOT NULL
)
