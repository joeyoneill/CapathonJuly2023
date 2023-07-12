--using HelpingHandCareCentersDB;


-- INITALIZE TABLES

/*
-- USER Information Table
CREATE TABLE Clients (
    Id NVARCHAR(32) PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Phone NVARCHAR(15)
);*/
/*
-- Dependents Table
CREATE TABLE Dependents (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    EmergencyContactName NVARCHAR(100),
    EmergencyContactPhone NVARCHAR(15),
    Birthday DATE,
    AdditionalNotes TEXT,
    ClientId NVARCHAR(32) REFERENCES Clients(Id)
);*/


SELECT * FROM Clients;
SELECT * FROM Dependents;