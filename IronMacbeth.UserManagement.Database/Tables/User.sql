CREATE TABLE [dbo].[User]
(
    [Login] VARCHAR(255) NOT NULL PRIMARY KEY,
    [Name] VARCHAR(255)  NULL,
    [Surname] VARCHAR(255)  NULL,
    [PasswordHash] VARCHAR(48) NOT NULL,
    [RoleId] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[UserRole]([Id]),
    [PhoneNumber] INT NULL
)
