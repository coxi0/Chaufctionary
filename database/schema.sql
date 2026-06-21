DROP DATABASE IF EXISTS chaufctionary;

CREATE DATABASE chaufctionary
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE chaufctionary;

CREATE TABLE Role (
    Id      INT AUTO_INCREMENT PRIMARY KEY,
    Libelle VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE Utilisateur (
    Id         INT AUTO_INCREMENT PRIMARY KEY,
    Nom        VARCHAR(100) NOT NULL,
    Prenom     VARCHAR(100) NOT NULL,
    Email      VARCHAR(150) NOT NULL UNIQUE,
    MotDePasse VARCHAR(255) NOT NULL,
    EstActif   BOOLEAN NOT NULL DEFAULT TRUE,
    RoleId     INT NOT NULL,
    CONSTRAINT FK_Utilisateur_Role
        FOREIGN KEY (RoleId) REFERENCES Role(Id)
);

CREATE TABLE Client (
    Id         INT AUTO_INCREMENT PRIMARY KEY,
    Numero     VARCHAR(20) NOT NULL UNIQUE,
    Nom        VARCHAR(150) NOT NULL,
    Adresse    VARCHAR(255) NOT NULL,
    Ville      VARCHAR(100) NOT NULL,
    CodePostal VARCHAR(10) NOT NULL,
    Telephone  VARCHAR(20),
    Latitude     DECIMAL(9,6),
    Longitude    DECIMAL(9,6),
    Notes        TEXT,
    ConseilAcces TEXT
);

CREATE TABLE Favori (
    UtilisateurId INT NOT NULL,
    ClientId      INT NOT NULL,
    PRIMARY KEY (UtilisateurId, ClientId),
    CONSTRAINT FK_Favori_Utilisateur
        FOREIGN KEY (UtilisateurId) REFERENCES Utilisateur(Id),
    CONSTRAINT FK_Favori_Client
        FOREIGN KEY (ClientId) REFERENCES Client(Id)
);

CREATE TABLE DemandeModification (
    Id            INT AUTO_INCREMENT PRIMARY KEY,
    ClientId      INT NOT NULL,
    UtilisateurId INT NOT NULL,
    Message       TEXT NOT NULL,
    DateCreation  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Demande_Client
        FOREIGN KEY (ClientId) REFERENCES Client(Id),
    CONSTRAINT FK_Demande_Utilisateur
        FOREIGN KEY (UtilisateurId) REFERENCES Utilisateur(Id)
);
