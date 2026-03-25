"# Projet2-Auth" 

Système d'authentification complet

🎯Description

   Ce projet est une application web développée avec ASP.NET Core MVC.
Il utilise Entity Framework Core et Identity pour gérer les utilisateurs et la sécurité.


🎯 Technologies utilisées

ASP.NET Core MVC
Entity Framework Core
Identity (authentification et autorisation)
SQLite (ou SQL Server)

 🎯Fonctionnalités principales

Inscription des utilisateurs (Register)
Connexion / Déconnexion (Login / Logout)
Gestion des rôles (Admin / User)
Accès restreint aux pages Admin
CRUD des catégories (Create, Read, Update, Delete)


---------------------------------Attribution des tâches de chacun-------------------------
Jerry
Partie du projet : Authentification

Tâches principales :

Implémenter Register (inscription)
Implémenter Login (connexion)
Implémenter Logout (déconnexion)
Ajouter validation : email unique, mot de passe fort, confirmation mot de passe
Afficher les messages d’erreurs (compte inexistant, mauvais mot de passe)

Fichiers concernés :

Areas/Identity/Pages/Account/Register.cshtml
Login.cshtml
Logout.cshtml

Objectif :
Permettre aux utilisateurs de créer un compte et se connecter

Jean
Partie du projet : Gestion des rôles et sécurité

Tâches principales :

Ajouter les rôles Admin et User
Protéger les pages avec [Authorize]
Créer une page accessible uniquement aux utilisateurs connectés
Créer une page accessible uniquement aux Admins
([Authorize(Roles="Admin")])
Vérifier que les utilisateurs non autorisés reçoivent une erreur 403

Fichiers concernés :

Controllers/HomeController.cs
Controllers/DashboardController.cs
Controllers/AdminController.cs
Program.cs

Objectif :
Gérer les permissions et sécuriser les pages

Dosso
Partie du projet : Base de données et configuration

Tâches principales :

Créer ApplicationUser
Créer AppDbContext avec IdentityDbContext
Configurer Identity dans Program.cs
Ajouter le seed pour créer automatiquement le compte Admin au démarrage
Créer le README.md avec les comptes de test

Fichiers concernés :

Models/ApplicationUser.cs
Data/AppDbContext.cs
Program.cs
README.md

Objectif :
Préparer la base de données et rendre l’application directement testable

---------------------------------Attribution des tâches de chacun-------------------------
 🎯 Compte Test

Admin
Email : admin@test.com
Mot de passe : Admin123!

User 

Email : user@test.com
Mot de passe : User123!



---------------------------------Comment tester le projet -------------------------

- Lancer le projet avec votre editeur de code 
- Ensuite dans le terminal , ecrivez : Update-Database
- Lancer le projet ,vous aurez des accès admins grâce à un seed déjà établi
- Pour le compte User , crée le via Register

- !!!!!! la base de données c'est sql server manager !!!!!!!


Notes : Pour ce projet , on a usé de l'ia pour comprendre comment identity fonctionnait , 
aussi on l'a utilisé pour nous donné un exemple de readme sur lequel se basé pour gérer 
notre read me à nous !. Merci 🙂

🦁Auteur : Ibrahima Elimane  Dosso 🦁
