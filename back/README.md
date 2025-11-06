#  AltenShop – API Backend

Ce projet constitue la **partie backend** de l’application **AltenShop**, développée en **.NET 8 (ASP.NET Core)** avec une base de données **PostgreSQL**.  
Le projet suit une architecture propre (**Clean Architecture**) et est entièrement conteneurisé grâce à **Docker** et **Docker Compose**.

Technologies principales

.NET 8 / ASP.NET Core
Entity Framework Core
PostgreSQL
Docker & Docker Compose
CQRS + MediatR
xUnit pour les tests


Structure du projet

back/
├── AltenShop.API/              # API Web ASP.NET Core (point d’entrée)
├── AltenShop.Application/      # Couche application (CQRS, logique métier)
├── AltenShop.Domain/           # Entités et objets de domaine
├── AltenShop.Infrastructure/   # Accès aux données, EF Core
├── AltenShop.Tests/            # Tests unitaires et d’intégration
├── Dockerfile                  # Build multi-étape pour Docker
└── docker-compose.yaml         # Configuration Docker Compose


---
##  Lancement rapide avec Docker

###  Prérequis
Avant de commencer, assurez-vous d’avoir installé :
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- (Optionnel) [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) si vous souhaitez exécuter le projet manuellement.

---

##  Étapes pour démarrer

### Ouvrir un terminal dans le dossier du backend
Exemple :
```bash
cd \product-trial-master\back

docker-compose down --remove-orphans
docker-compose build --no-cache
docker-compose up -d



Accéder à l’API

Une fois les conteneurs en marche, l’API est accessible à l’adresse :

http://localhost:5000
http://localhost:5000/index.html

Arrêter et nettoyer
docker-compose down -v

