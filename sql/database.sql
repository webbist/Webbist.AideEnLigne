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
);
