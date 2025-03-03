DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Questions;

DROP DATABASE IF EXISTS AssistClubDB

CREATE DATABASE AssistClubDB;

USE AssistClubDB;

CREATE TABLE Users (
                       Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                       Firstname NVARCHAR(100) NOT NULL,
                       Lastname NVARCHAR(100) NOT NULL,
                       Email VARCHAR(100) UNIQUE NOT NULL,
                       Photo VARCHAR(255),
                       Club NVARCHAR(100) NOT NULL,
                       Microsite VARCHAR(255) NOT NULL,
);

CREATE TABLE Questions (
                           Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                           UserId UNIQUEIDENTIFIER NOT NULL,
                           Title NVARCHAR(255) NOT NULL,
                           Content NVARCHAR(2000) NOT NULL,
                           CreatedAt DATETIME DEFAULT GETDATE(),
                           UpdatedAt DATETIME NULL,
                           Visibility NVARCHAR(50) NOT NULL CHECK (Visibility IN ('public', 'private')),
                           Status NVARCHAR(50) NOT NULL CHECK (Status IN ('open', 'pending', 'resolved')),

                           CONSTRAINT FK_Questions_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);