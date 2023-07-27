CREATE TABLE [dbo].[RegisteredUsers]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FullName] VARCHAR(100) NULL, 
    [Email] VARCHAR(50) NULL, 
    [PassWord] VARCHAR(50) NULL, 
    [DateTime] DATETIME NULL
)
