DROP TABLE IF EXISTS Answers;
DROP TABLE IF EXISTS Questions;
DROP TABLE IF EXISTS Users;

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
                       Role NVARCHAR(50) NOT NULL CHECK (Role IN ('admin', 'user')),
);

CREATE TABLE Questions (
                           Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                           UserId UNIQUEIDENTIFIER NOT NULL,
                           Title NVARCHAR(255) NOT NULL,
                           Content NVARCHAR(MAX) NOT NULL,
                           CreatedAt DATETIME DEFAULT GETDATE(),
                           UpdatedAt DATETIME NULL,
                           Visibility NVARCHAR(50) NOT NULL CHECK (Visibility IN ('public', 'private')),
                           Status NVARCHAR(50) NOT NULL CHECK (Status IN ('open', 'pending', 'resolved')),

                           CONSTRAINT FK_Questions_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE Answers (
                         Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                         QuestionId UNIQUEIDENTIFIER NOT NULL,
                         UserId UNIQUEIDENTIFIER NOT NULL,
                         Content NVARCHAR(MAX) NOT NULL,
                         CreatedAt DATETIME DEFAULT GETDATE(),
                         UpdatedAt DATETIME NULL,

                         CONSTRAINT FK_Answers_Questions FOREIGN KEY (QuestionId) REFERENCES Questions(Id) ON DELETE CASCADE,
                         CONSTRAINT FK_Answers_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);