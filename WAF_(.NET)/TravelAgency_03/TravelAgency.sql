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

INSERT INTO Building VALUES('Petra bungal�', 1, 100, 0, 5, 45.4591, 12.5068, 'Sz�ll�s bungall�ban 100m strandt�l (homokos tengerpart). A ter�leten tal�lhat� �tterem, b�r, j�tsz�t�r. �lyszakai nyugalmat betartani. Ott-tart�zkod�s szombatt�l szombatig. A sz�ll�sszolg�ltat� besz�l is cseh�l. Parkol� - nem �rz�tt, ingyenes. Busz 100m. Vonat (�llom�s San Dona di Piave) 25km. Rep�l�t�r (Venezia - Marco Polo) 40km. Cavallino - k�zpont.');
INSERT INTO Building VALUES('Willerby bungal�', 1, 100, 2, 9, 45.4566, 12.5004, 'Sz�ll�s bungall�ban 100m strandt�l (homokos tengerpart). A ter�leten tal�lhat� �tterem, b�r, j�tsz�t�r. �lyszakai nyugalmat betartani. Ott-tart�zkod�s szombatt�l szombatig. A sz�ll�sszolg�ltat� besz�l is cseh�l. Parkol� - nem �rz�tt, ingyenes. Busz 100m. Vonat (�llom�s San Dona di Piave) 25km. Rep�l�t�r (Venezia - Marco Polo) 40km. Cavallino - k�zpont.');
INSERT INTO Building VALUES('Cavallino Hotel', 1, 50, 1, 3, 45.4788, 12.5677, 'Sz�lloda k�zs�gben 50m a tengert�l. A hotelban / ter�let�n tal�lhat� �tterem, terasz, b�r, k�v�h�z, t�rsalg� TV-vel, bili�rd, darts, szauna, ny�ri medence, kerti pihen�hely, homokoz�, hinta, l�gkondicion�l�; illet�k fej�ben: sportfelszerel�s-k�lcs�nz�, lovagl�s, p�nzv�lt�. Internet-csatlakoz�s. Naperny� �s napoz��gy a sz�ll�s �r�ban. Parkol� - nem �rz�tt, ingyenes. Busz 20m. Rep�l�t�r (Aeroporto Marco Polo Venezia) 30km. Cavallino - k�zpont 100m.');
INSERT INTO Building VALUES('Hotel Veneto', 2, 2000, 0, 2, 45.5171, 12.6694, 'Sz�lloda k�zs�gben, 2km strandt�l. A hotelban van �tkezde (csak reggeli), ker�kp�rk�lcs�nz�. A sz�llod�ban nincs �tterem, individu�lis �tkez�s �tteremben 500m a hotelt�l. Internet-csatlakoz�s Wi-Fi �s l�gkondicion�l� illet�k fej�ben. Parkol� - nem �rz�tt, ingyenes. Busz 500m. Vonat (�llom�s Venezia) 25km. Lido di Jesolo - k�zpont 6km.');

INSERT INTO Apartment VALUES(1, 1, 6, '2x2-�gyas szoba, nappali k�t p�t�ggyal (sz�tnyithat� hever�), f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, konyha (g�zt�zhely, h�t�szekr�ny), �tkez�sarok.', 21);
INSERT INTO Apartment VALUES(1, 2, 6, '2x2-�gyas szoba, nappali k�t p�t�ggyal (sz�tnyithat� hever�), f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, konyha (g�zt�zhely, h�t�szekr�ny), �tkez�sarok.', 21);
INSERT INTO Apartment VALUES(1, 3, 6, '2x2-�gyas szoba, nappali k�t p�t�ggyal (sz�tnyithat� hever�), f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, konyha (g�zt�zhely, h�t�szekr�ny), �tkez�sarok.', 21);
INSERT INTO Apartment VALUES(1, 4, 6, '2x2-�gyas szoba, nappali k�t p�t�ggyal (sz�tnyithat� hever�), f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, konyha (g�zt�zhely, h�t�szekr�ny), �tkez�sarok.', 21);
INSERT INTO Apartment VALUES(2, 1, 6, '2x2-�gyas szoba, TV, nappali k�t p�t�ggyal, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, konyha (g�zt�zhely, h�t�szekr�ny), �tkez�sarok.', 28);
INSERT INTO Apartment VALUES(2, 2, 6, '2x2-�gyas szoba, TV, nappali k�t p�t�ggyal, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, konyha (g�zt�zhely, h�t�szekr�ny), �tkez�sarok.', 28);
INSERT INTO Apartment VALUES(2, 3, 6, '2x2-�gyas szoba, TV, nappali k�t p�t�ggyal, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, konyha (g�zt�zhely, h�t�szekr�ny), �tkez�sarok.', 28);
INSERT INTO Apartment VALUES(3, 1, 7, '1-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, hajsz�r�t�, telefon, erk�ly, m�holdas TV.', 18);
INSERT INTO Apartment VALUES(3, 2, 7, '1-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, hajsz�r�t�, telefon, erk�ly, m�holdas TV.', 18);
INSERT INTO Apartment VALUES(3, 3, 7, '1-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, hajsz�r�t�, telefon, erk�ly, m�holdas TV.', 18);
INSERT INTO Apartment VALUES(3, 4, 7, '2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, hajsz�r�t�, telefon, erk�ly, m�holdas TV.', 32);
INSERT INTO Apartment VALUES(3, 5, 7, '2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, hajsz�r�t�, telefon, erk�ly, m�holdas TV.', 32);
INSERT INTO Apartment VALUES(3, 6, 7, '2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, hajsz�r�t�, telefon, erk�ly, m�holdas TV.', 32);
INSERT INTO Apartment VALUES(3, 7, 7, '2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, hajsz�r�t�, telefon, erk�ly, m�holdas TV.', 32);
INSERT INTO Apartment VALUES(3, 8, 7, '2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, hajsz�r�t�, telefon, erk�ly, m�holdas TV.', 32);
INSERT INTO Apartment VALUES(3, 9, 7, '2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, hajsz�r�t�, telefon, erk�ly, m�holdas TV.', 32);
INSERT INTO Apartment VALUES(3, 10, 7, 'Apartman: 2x2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, telefon, erk�ly, m�holdas TV, nappali, kis konyha.', 54);
INSERT INTO Apartment VALUES(3, 11, 7, 'Apartman: 2x2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, telefon, erk�ly, m�holdas TV, nappali, kis konyha.', 54);
INSERT INTO Apartment VALUES(3, 12, 7, 'Apartman: 2x2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, telefon, erk�ly, m�holdas TV, nappali, kis konyha.', 54);
INSERT INTO Apartment VALUES(3, 13, 7, 'Apartman: 2x2-�gyas szoba, f�rd�szoba zuhanyoz�f�lk�vel �s WC-vel, telefon, erk�ly, m�holdas TV, nappali, kis konyha.', 54);
INSERT INTO Apartment VALUES(4, 1, 6, '3-�gyas szoba lehet�s�g p�t�gyra, TV, f�rd�szoba (zuhanyoz�f�lke, WC, bid�), erk�ly.', 65);
INSERT INTO Apartment VALUES(4, 2, 6, '2-�gyas szoba k�t p�t�ggyal, TV, f�rd�szoba (zuhanyoz�f�lke, WC, bid�), erk�ly.', 42);
INSERT INTO Apartment VALUES(4, 3, 6, '2-�gyas szoba k�t p�t�ggyal, TV, f�rd�szoba (zuhanyoz�f�lke, WC, bid�), erk�ly.', 42);
INSERT INTO Apartment VALUES(4, 4, 6, '2-�gyas szoba k�t p�t�ggyal, TV, f�rd�szoba (zuhanyoz�f�lke, WC, bid�), erk�ly.', 42);
INSERT INTO Apartment VALUES(4, 5, 6, '2-�gyas szoba k�t p�t�ggyal, TV, f�rd�szoba (zuhanyoz�f�lke, WC, bid�), erk�ly.', 42);
INSERT INTO Apartment VALUES(4, 6, 6, '2-�gyas szoba, TV, f�rd�szoba (zuhanyoz�f�lke, WC, bid�), erk�ly.', 34);
INSERT INTO Apartment VALUES(4, 7, 6, '2-�gyas szoba, TV, f�rd�szoba (zuhanyoz�f�lke, WC, bid�), erk�ly.', 34);
GO