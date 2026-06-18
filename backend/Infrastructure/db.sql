CREATE DATABASE IF NOT EXISTS chaufctionary
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

INSERT INTO Role (Libelle) VALUES
    ('Chauffeur'),
    ('Planneur'),
    ('Admin');

INSERT INTO Utilisateur (Nom, Prenom, Email, MotDePasse, EstActif, RoleId) VALUES
    ('Admin', 'Root', 'admin@test.com', '$2a$11$BykRmB105usItAXWBTi.6OvB99sKZra8A8r79OcY7yDYDAglqgYru', TRUE, 3),
    ('Planneur', 'Test', 'planneur@test.com', '$2a$11$J123K3LrVfof9puzSKWsk.dmWsm8LUJQfS9uYDjanhUz2h4weBM/S', TRUE, 2),
    ('Chauffeur', 'Test', 'chauffeur@test.com', '$2a$11$D4DigTOgo9MGdzMY77vNWOeBvKntx21Mg2AmFVPeRURqISfc.psxW', TRUE, 1);


CREATE TABLE Client (
    Id         INT AUTO_INCREMENT PRIMARY KEY,
    Nom        VARCHAR(150) NOT NULL,
    Adresse    VARCHAR(255) NOT NULL,
    Ville      VARCHAR(100) NOT NULL,
    CodePostal VARCHAR(10) NOT NULL,
    Telephone  VARCHAR(20),
    Latitude   DECIMAL(9,6),
    Longitude  DECIMAL(9,6)
);

INSERT INTO Client (Nom, Adresse, Ville, CodePostal, Telephone, Latitude, Longitude) VALUES
    ('Boulangerie Martin', '12 rue des Lilas', 'Lille', '59000', '0320000001', 50.629250, 3.057256),
    ('Garage Dupont', '5 avenue de la Gare', 'Roubaix', '59100', '0320000002', 50.690000, 3.174000),
    ('Pharmacie Centrale', '8 place du Marche', 'Tourcoing', '59200', '0320000003', 50.723000, 3.161000),
    ('Restaurant Le Nord', '22 boulevard Gambetta', 'Lille', '59000', '0320000004', 50.633000, 3.066000);
