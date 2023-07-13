--using HelpingHandCareCentersDB;

----------------------------------------------------
-- DELETE ALL INFO FROM TABLES                    --
----------------------------------------------------

--DELETE FROM Clients;

--DELETE FROM SessionDependents;

--DELETE FROM Dependents;

--DELETE FROM "Sessions";
--DELETE FROM Locations;
--DELETE FROM Businesses;
--DELETE FROM CareTypes;


----------------------------------------------------
-- DROP TABLES                                    --
----------------------------------------------------

--DROP TABLE Clients;

--DROP TABLE SessionDependents;

--DROP TABLE Dependents;

--DROP TABLE "Sessions";
--DROP TABLE Locations;
--DROP TABLE Businesses;
--DROP TABLE CareTypes;

----------------------------------------------------
-- INITALIZE TABLES                               --
----------------------------------------------------

/*
-- USER Information Table
CREATE TABLE Clients (
    Id NVARCHAR(32) PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Phone NVARCHAR(15)
);

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

/*
-- Care Types (Child, Elderly, Pet)
-- Lookup table
CREATE TABLE CareTypes (
    "Id" INT IDENTITY(1,1) PRIMARY KEY,
    "Name" NVARCHAR(50)
);


-- Businesses Table
CREATE TABLE Businesses (
    "Id" INT IDENTITY(1,1) PRIMARY KEY,
    "Name" NVARCHAR(100)
);


-- Location
CREATE TABLE Locations (
    "Id" Int IDENTITY(1,1) PRIMARY KEY,
    "Address" NVARCHAR(100),
    "Address2" NVARCHAR(100),
    "City" NVARCHAR (100),
    "State" NVARCHAR(100),
    "Country" NVARCHAR(100),
    "ZipCode" NVARCHAR(10),
    "BusinessId" INT,
    FOREIGN KEY (BusinessId) REFERENCES Businesses(Id)
);


-- Sessions
CREATE TABLE Sessions (
    "Id" UNIQUEIDENTIFIER PRIMARY KEY,
    "StartTime" DATETIME,
    "EndTime" DATETIME,
    "MaxDependents" INT,
    "CareTypeId" INT,
    "LocationId" INT,
    FOREIGN KEY (CareTypeId) REFERENCES CareTypes(Id),
    FOREIGN KEY (LocationId) REFERENCES Locations(Id)
);


-- SessionDependents Table
-- Connecting Table: Resolves many-to-many relationship
CREATE TABLE SessionDependents (
    "Id" INT IDENTITY(1,1) PRIMARY KEY,
    "SessionId" UNIQUEIDENTIFIER,
    "DependentId" UNIQUEIDENTIFIER,
    FOREIGN KEY (SessionId) REFERENCES Sessions(Id),
    FOREIGN KEY (DependentId) REFERENCES Dependents(Id)
);
----------------------------------------------------
-- ADD DATA INTO TABLES                           --
----------------------------------------------------


-- Care Type Lookup Table Data
INSERT INTO CareTypes (Name) VALUES ('CHILD');
INSERT INTO CareTypes (Name) VALUES ('PET');


-- Business Data
INSERT INTO Businesses (Name) VALUES ('Community');
INSERT INTO Businesses (Name) VALUES ('Capgemini');
INSERT INTO Businesses (Name) VALUES ('Microsoft');
INSERT INTO Businesses (Name) VALUES ('Amazon');


-- Community Location Data
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('11 Lindsley Ave B', 'Nashville', 'TN', 'USA', '37210', 1);
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('1811 Osage St', 'Nashville', 'TN', 'USA', '37208', 1);
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('1234 Schrader Ln', 'Nashville', 'TN', 'USA', '37208', 1);


-- Capgemini Location Data
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('2142 Boyce St #3901', 'Columbia', 'SC', 'USA', '29201', 2);
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('1600 West End Ave', 'Nashville', 'TN', 'USA', '37203', 2);
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('3475 Piedmont Rd NE #1400', 'Atlanta', 'GA', 'USA', '30305', 2);


-- Microsoft Location Data
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('1045 La Avenida St', 'Mountain View', 'CA', 'USA', '94043', 3);
INSERT INTO Locations (Address, Address2, City, State, Country, Zipcode, BusinessId)
VALUES ('Microsoft ArrowPoint 1', '8055 Microsoft Way', 'Charlotte', 'NC', 'USA', '28273', 3);
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('1414 NW Northup St #900', 'Portland', 'OR', 'USA', '97209', 3);


-- Amazon Location Data
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('1800 S Bell St', 'Arlington', 'VA', 'USA', '22202', 4);
INSERT INTO Locations (Address, City, State, Country, Zipcode, BusinessId)
VALUES ('1010 Church St', 'Nashville', 'TN', 'USA', '37203', 4);

-- Dummy Session Data


INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 1, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 1, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 1, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 2, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 2, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 2, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 3, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 3, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 3, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 4, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 4, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 4, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 5, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 5, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 5, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 6, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 6, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 6, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 7, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 7, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 7, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 8, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 8, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 8, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 9, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 9, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 9, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 10, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 10, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 10, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);

INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 11, '2023-07-12 10:00:00', '2023-07-12 12:00:00', 15);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 1, 11, '2023-07-15 9:00:00', '2023-07-12 12:00:00', 20);
INSERT INTO Sessions (Id, CareTypeId, LocationId, StartTime, EndTime, MaxDependents)
VALUES (NEWID(), 2, 11, '2023-07-16 7:30:00', '2023-07-12 17:00:00', 10);
*/
----------------------------------------------------
-- DISPLAY TABLES                                 --
----------------------------------------------------

--SELECT * FROM Clients;
SELECT * FROM Dependents;
--SELECT * FROM CareTypes;
--SELECT * FROM Businesses;
--SELECT * FROM Locations;
SELECT * FROM "Sessions";
SELECT * FROM SessionDependents;