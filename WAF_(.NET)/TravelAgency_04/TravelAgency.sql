IF DB_ID('TravelAgency') IS NOT NULL
	DROP DATABASE TravelAgency;
GO

CREATE DATABASE TravelAgency;
GO
USE TravelAgency;
GO

CREATE TABLE City(
    Id INTEGER PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(40) NOT NULL
);
GO

CREATE TABLE Building (
    Id INTEGER PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(30) NOT NULL,
    CityId INTEGER NOT NULL,
    SeaDistance INTEGER NOT NULL,
    ShoreId INTEGER,
    Features INTEGER,
    LocationX FLOAT NOT NULL,
    LocationY FLOAT NOT NULL,
    Comment VARCHAR(1000),
    CONSTRAINT BuldingToCity 
        FOREIGN KEY (CityId) 
        REFERENCES City (Id)
);
GO

CREATE TABLE BuildingImage
(
	Id INTEGER PRIMARY KEY IDENTITY(1,1),
	BuildingId INTEGER NOT NULL,
	ImageSmall VARBINARY(max) NOT NULL,
	ImageLarge VARBINARY(max) NOT NULL
	CONSTRAINT ImageToBuilding 
        FOREIGN KEY (BuildingId) 
        REFERENCES Building (Id)
);
GO

CREATE TABLE Apartment (
    Id INTEGER PRIMARY KEY IDENTITY(1,1),
    BuildingId INTEGER NOT NULL,
    Room INTEGER NOT NULL,
    Turnday INTEGER NOT NULL,
    Comment VARCHAR(1000),
    Price INTEGER NOT NULL,
    CONSTRAINT ApartmentToBuilding 
        FOREIGN KEY (BuildingId) 
        REFERENCES Building (Id)
);
GO

CREATE TABLE Guest (
    UserName VARCHAR(40) PRIMARY KEY,
    UserPassword BINARY(64),
    Name VARCHAR(60) NOT NULL,
    Address VARCHAR(200),
    PhoneNumber VARCHAR(20),
    Email VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Rent (
    UserName VARCHAR(40),
    ApartmentId INTEGER,
    StartDate Date NOT NULL,
    EndDate Date NOT NULL,
    CONSTRAINT RentToGuest
        FOREIGN KEY (UserName) 
        REFERENCES Guest (UserName),
    CONSTRAINT RentToApartement 
        FOREIGN KEY (ApartmentId) 
        REFERENCES Apartment (Id),
    PRIMARY KEY (UserName, ApartmentId, StartDate)
);
GO

INSERT INTO City VALUES('Cavallino');
INSERT INTO City VALUES('Lido di Jesolo');

INSERT INTO Building VALUES('Petra bungaló', 1, 100, 0, 5, 45.4591, 12.5068, 'Szállás bungallóban 100m strandtól (homokos tengerpart). A területen található étterem, bár, játszótér. Élyszakai nyugalmat betartani. Ott-tartózkodás szombattól szombatig. A szállásszolgáltató beszél is csehül. Parkoló - nem õrzött, ingyenes. Busz 100m. Vonat (állomás San Dona di Piave) 25km. Repülõtér (Venezia - Marco Polo) 40km. Cavallino - központ.');
INSERT INTO Building VALUES('Willerby bungaló', 1, 100, 2, 9, 45.4566, 12.5004, 'Szállás bungallóban 100m strandtól (homokos tengerpart). A területen található étterem, bár, játszótér. Élyszakai nyugalmat betartani. Ott-tartózkodás szombattól szombatig. A szállásszolgáltató beszél is csehül. Parkoló - nem õrzött, ingyenes. Busz 100m. Vonat (állomás San Dona di Piave) 25km. Repülõtér (Venezia - Marco Polo) 40km. Cavallino - központ.');
INSERT INTO Building VALUES('Cavallino Hotel', 1, 50, 1, 3, 45.4788, 12.5677, 'Szálloda községben 50m a tengertõl. A hotelban / területén található étterem, terasz, bár, kávéház, társalgó TV-vel, biliárd, darts, szauna, nyári medence, kerti pihenõhely, homokozó, hinta, légkondicionáló; illeték fejében: sportfelszerelés-kölcsönzõ, lovaglás, pénzváltó. Internet-csatlakozás. Napernyõ és napozóágy a szállás árában. Parkoló - nem õrzött, ingyenes. Busz 20m. Repülõtér (Aeroporto Marco Polo Venezia) 30km. Cavallino - központ 100m.');
INSERT INTO Building VALUES('Hotel Veneto', 2, 2000, 0, 2, 45.5171, 12.6694, 'Szálloda községben, 2km strandtól. A hotelban van étkezde (csak reggeli), kerékpárkölcsönzõ. A szállodában nincs étterem, individuális étkezés étteremben 500m a hoteltõl. Internet-csatlakozás Wi-Fi és légkondicionáló illeték fejében. Parkoló - nem õrzött, ingyenes. Busz 500m. Vonat (állomás Venezia) 25km. Lido di Jesolo - központ 6km.');

INSERT INTO Apartment VALUES(1, 1, 6, '2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverõ), fürdõszoba zuhanyozófülkével és WC-vel, konyha (gáztûzhely, hûtõszekrény), étkezõsarok.', 21);
INSERT INTO Apartment VALUES(1, 2, 6, '2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverõ), fürdõszoba zuhanyozófülkével és WC-vel, konyha (gáztûzhely, hûtõszekrény), étkezõsarok.', 21);
INSERT INTO Apartment VALUES(1, 3, 6, '2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverõ), fürdõszoba zuhanyozófülkével és WC-vel, konyha (gáztûzhely, hûtõszekrény), étkezõsarok.', 21);
INSERT INTO Apartment VALUES(1, 4, 6, '2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverõ), fürdõszoba zuhanyozófülkével és WC-vel, konyha (gáztûzhely, hûtõszekrény), étkezõsarok.', 21);
INSERT INTO Apartment VALUES(2, 1, 6, '2x2-ágyas szoba, TV, nappali két pótággyal, fürdõszoba zuhanyozófülkével és WC-vel, konyha (gáztûzhely, hûtõszekrény), étkezõsarok.', 28);
INSERT INTO Apartment VALUES(2, 2, 6, '2x2-ágyas szoba, TV, nappali két pótággyal, fürdõszoba zuhanyozófülkével és WC-vel, konyha (gáztûzhely, hûtõszekrény), étkezõsarok.', 28);
INSERT INTO Apartment VALUES(2, 3, 6, '2x2-ágyas szoba, TV, nappali két pótággyal, fürdõszoba zuhanyozófülkével és WC-vel, konyha (gáztûzhely, hûtõszekrény), étkezõsarok.', 28);
INSERT INTO Apartment VALUES(3, 1, 7, '1-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, mûholdas TV.', 18);
INSERT INTO Apartment VALUES(3, 2, 7, '1-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, mûholdas TV.', 18);
INSERT INTO Apartment VALUES(3, 3, 7, '1-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, mûholdas TV.', 18);
INSERT INTO Apartment VALUES(3, 4, 7, '2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, mûholdas TV.', 32);
INSERT INTO Apartment VALUES(3, 5, 7, '2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, mûholdas TV.', 32);
INSERT INTO Apartment VALUES(3, 6, 7, '2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, mûholdas TV.', 32);
INSERT INTO Apartment VALUES(3, 7, 7, '2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, mûholdas TV.', 32);
INSERT INTO Apartment VALUES(3, 8, 7, '2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, mûholdas TV.', 32);
INSERT INTO Apartment VALUES(3, 9, 7, '2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, mûholdas TV.', 32);
INSERT INTO Apartment VALUES(3, 10, 7, 'Apartman: 2x2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, telefon, erkély, mûholdas TV, nappali, kis konyha.', 54);
INSERT INTO Apartment VALUES(3, 11, 7, 'Apartman: 2x2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, telefon, erkély, mûholdas TV, nappali, kis konyha.', 54);
INSERT INTO Apartment VALUES(3, 12, 7, 'Apartman: 2x2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, telefon, erkély, mûholdas TV, nappali, kis konyha.', 54);
INSERT INTO Apartment VALUES(3, 13, 7, 'Apartman: 2x2-ágyas szoba, fürdõszoba zuhanyozófülkével és WC-vel, telefon, erkély, mûholdas TV, nappali, kis konyha.', 54);
INSERT INTO Apartment VALUES(4, 1, 6, '3-ágyas szoba lehetõség pótágyra, TV, fürdõszoba (zuhanyozófülke, WC, bidé), erkély.', 65);
INSERT INTO Apartment VALUES(4, 2, 6, '2-ágyas szoba két pótággyal, TV, fürdõszoba (zuhanyozófülke, WC, bidé), erkély.', 42);
INSERT INTO Apartment VALUES(4, 3, 6, '2-ágyas szoba két pótággyal, TV, fürdõszoba (zuhanyozófülke, WC, bidé), erkély.', 42);
INSERT INTO Apartment VALUES(4, 4, 6, '2-ágyas szoba két pótággyal, TV, fürdõszoba (zuhanyozófülke, WC, bidé), erkély.', 42);
INSERT INTO Apartment VALUES(4, 5, 6, '2-ágyas szoba két pótággyal, TV, fürdõszoba (zuhanyozófülke, WC, bidé), erkély.', 42);
INSERT INTO Apartment VALUES(4, 6, 6, '2-ágyas szoba, TV, fürdõszoba (zuhanyozófülke, WC, bidé), erkély.', 34);
INSERT INTO Apartment VALUES(4, 7, 6, '2-ágyas szoba, TV, fürdõszoba (zuhanyozófülke, WC, bidé), erkély.', 34);
GO