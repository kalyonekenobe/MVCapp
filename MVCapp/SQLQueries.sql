USE master;
GO
IF (DB_ID('ToDoListDB') is null)
	CREATE DATABASE ToDoListDB
GO
USE ToDoListDB;

DROP TABLE IF EXISTS Task

DROP TABLE IF EXISTS Category

CREATE TABLE Category (
	
	[CategoryId] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Task 
(
	[TaskId] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(255) NOT NULL,
	[Deadline] DATETIME NOT NULL,
	[CategoryId] INT NOT NULL,
	[Expired] BIT NOT NULL DEFAULT 0

	FOREIGN KEY ([CategoryId]) REFERENCES Category([CategoryId]) ON DELETE CASCADE ON UPDATE CASCADE
);

IF NOT EXISTS (SELECT * FROM Category WHERE [Name] = 'CATEGORY 1')
BEGIN	
	INSERT INTO Category ([Name]) VALUES ('CATEGORY 1');
END
ELSE
BEGIN
	DECLARE @categoryId INT;
	SET @categoryId = (SELECT [CategoryId] FROM Category WHERE [Name] = 'CATEGORY 1');
	INSERT INTO Task ([Name], [Deadline], [CategoryId]) VALUES ('TASK 1', GETDATE(), @categoryId);
END