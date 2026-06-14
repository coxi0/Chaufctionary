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
