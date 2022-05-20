/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

--- Populate UserRole table ---
MERGE [dbo].[UserRole] AS target
USING
(
    SELECT Id, [Name]
    FROM
    (
        VALUES
        (1, 'User'),
        (2, 'Admin')
    ) as userRoles(Id, [Name])
) AS source
ON target.Id = source.Id
WHEN MATCHED THEN
    UPDATE SET [Name] = source.[Name]
WHEN NOT MATCHED BY target THEN
    INSERT (Id, [NAME]) VALUES (source.Id, source.[Name])
WHEN NOT MATCHED BY source THEN
    DELETE;
    
--- Populate initial users ---
MERGE [dbo].[User] AS target
USING
(
    SELECT [Login], [Name], [Surname], [PasswordHash], [RoleId], [PhoneNumber]
    FROM
    (
        VALUES
        ('admin', 'Richard', 'Branson', 'Uu2t3h6oy/KpR8lWEyMZRdOYN1bFRd1UGRlyiAbQ78QFo1kU', 2, 0800352352),
        ('me', 'Olena', 'Kozak', 'YUUDrYOVr6LUdmZc5lnEpDUrvttYWiOzb7hNhlkNirl6P4So', 1, 0507050620)
    ) as [user]([Login], [Name], [Surname], [PasswordHash], [RoleId], [PhoneNumber])
) AS source
ON target.[Login] = source.[Login]
WHEN NOT MATCHED BY target THEN
    INSERT ([Login], [Name], [Surname], [PasswordHash], [RoleId], [PhoneNumber]) 
    VALUES ([Login], [Name], [Surname], [PasswordHash], [RoleId], [PhoneNumber]);

