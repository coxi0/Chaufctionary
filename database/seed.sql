USE chaufctionary;

INSERT INTO Role (Libelle) VALUES
    ('Chauffeur'),
    ('Planneur'),
    ('Admin');

INSERT INTO Utilisateur (Nom, Prenom, Email, MotDePasse, EstActif, RoleId) VALUES
    ('Admin', 'Root', 'admin@test.com', '$2a$11$BykRmB105usItAXWBTi.6OvB99sKZra8A8r79OcY7yDYDAglqgYru', TRUE, 3),
    ('Planneur', 'Test', 'planneur@test.com', '$2a$11$J123K3LrVfof9puzSKWsk.dmWsm8LUJQfS9uYDjanhUz2h4weBM/S', TRUE, 2),
    ('Chauffeur', 'Test', 'chauffeur@test.com', '$2a$11$D4DigTOgo9MGdzMY77vNWOeBvKntx21Mg2AmFVPeRURqISfc.psxW', TRUE, 1);

INSERT INTO Client (Numero, Nom, Adresse, Ville, CodePostal, Telephone, Latitude, Longitude, Notes, ConseilAcces) VALUES
    ('03884', 'Boulangerie Martin', '12 rue des Lilas', 'Lille', '59000', '0320000001', 50.629250, 3.057256, 'LIV de 6 a 11, prendre les temperatures et demander M. Martin', 'Entrer en marche avant par la rue des Lilas, se garer devant le n12, repartir en marche arriere vers la place'),
    ('01250', 'Garage Dupont', '5 avenue de la Gare', 'Roubaix', '59100', '0320000002', 50.690000, 3.174000, NULL, NULL),
    ('07421', 'Pharmacie Centrale', '8 place du Marche', 'Tourcoing', '59200', '0320000003', 50.723000, 3.161000, 'Acces par l arriere du batiment', 'Livraison par la cour arriere, sonner a l interphone PRO'),
    ('02009', 'Restaurant Le Nord', '22 boulevard Gambetta', 'Lille', '59000', '0320000004', 50.633000, 3.066000, NULL, NULL);

INSERT INTO Favori (UtilisateurId, ClientId) VALUES
    (3, 1),
    (3, 3);

INSERT INTO DemandeModification (ClientId, UtilisateurId, Message) VALUES
    (1, 3, 'Acces modifie : entrer par la rue laterale, se garer cote livraison, repartir en marche arriere');
