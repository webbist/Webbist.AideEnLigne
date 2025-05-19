![logo_title.png](logo_title.png)

AssistClub est une plateforme SaaS destinée aux clubs sportifs, 
offrant un espace interactif de questions-réponses visant à 
centraliser et faciliter le support technique.

## Environnement de développement
Ce projet utilise une architecture complète avec Blazor Server, API ASP.NET Core, 
Docker, et une base de données SQL Server. Voici les étapes et prérequis 
pour lancer l'environnement localement ou sur un serveur.

### Prérequis
- **Docker** : Assurez-vous d'avoir Docker installé sur votre machine. 
  Vous pouvez le télécharger depuis [Docker Desktop](https://www.docker.com/products/docker-desktop).
- **.NET SDK** : Installez le SDK .NET 8.0 ou supérieur. 
  Vous pouvez le télécharger depuis [Microsoft .NET](https://dotnet.microsoft.com/download).
- **SQL Server** : Bien que SQL Server soit inclus dans le projet Docker, 
  vous pouvez également utiliser une instance locale de SQL Server si vous le souhaitez. 
  Assurez-vous que le port SQL Server (par défaut 1433) est ouvert et accessible depuis votre machine.

### Configuration de l'environnement

1. **Configuration de la base de données** :
   Dans docker-compose.yml, les services sont définis comme suit :
   ```yaml
    services:
      database:
         image: mcr.microsoft.com/mssql/server:2022-latest
         ports:
            - "1433:1433"
         environment:
            - ACCEPT_EULA=Y
            - SA_PASSWORD=your_password
         volumes:
            - assistclub-data:/var/opt/mssql
            - ./sql:/sql
    db-init:
      image: mcr.microsoft.com/mssql-tools
      depends_on:
      - database
      entrypoint: >
        /bin/bash -c "
        echo 'Waiting for database to be ready...';
        sleep 20;
        /opt/mssql-tools/bin/sqlcmd -S assistclub-db -U sa -P 'YourStrong@Passw0rd' -i /sql/database.sql;
        echo 'Database initialized successfully.';
        "
      volumes:
        - ./sql:/sql
    ```
    - Modifiez le mot de passe `your_password` pour l'utilisateur `sa`.
    - Modifiez le mot de passe `YourStrong@Passw0rd` dans la section `db-init` 
      pour correspondre à celui défini précédemment.
    - Le script SQL de création de la base de données se trouve dans `sql/database.sql` et initialise les tables principales.
---
2. **Configuration de l'API (AssistClub.API)** :
Dans le projet AssistClub.API, ouvrez le fichier `appsettings.json` 
et modifiez la chaîne de connexion pour correspondre à celle définie dans 
le fichier `docker-compose.yml`.
- Exemple de chaîne de connexion :
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=assistclub-db;Database=AssistClubDB;User Id=sa;Password=your_password; TrustServerCertificate=True;"
  }
  ```
  - Remplacez `your_password` par le mot de passe que vous avez défini pour l'utilisateur `sa`.
  - Assurez-vous que le nom du serveur correspond à celui défini dans le fichier `docker-compose.yml`.
  - Si vous utilisez une instance locale de SQL Server,
    remplacez `assistclub-db` par `localhost` ou le nom de votre instance.
  - Assurez-vous que le port SQL Server (par défaut 1433) est ouvert
    et accessible depuis votre machine.
  - Assurez-vous que le nom de la base de données (`AssistClubDB`)
    correspond à celui que vous souhaitez utiliser. Si la base de données n'existe pas,
    elle sera créée automatiquement lors de l'initialisation.

* Modifiez le "EmailSettings" pour correspondre à votre configuration SMTP :
     ```json
     "EmailSettings": {
       "From": "assistclub@yourdomain.com",
       "Host": "smtp.ethereal.email",
       "Port": "587",
       "Username": "my_username",
       "Password": "my_super_secret_password",
       "ApiKey": "my_apikey"
     }
     ```
     - Remplacez le valeur de `From` par l'adresse e-mail que vous souhaitez utiliser pour envoyer des e-mails.
     - Remplacez les valeurs de `Host`, `Port`, `Username` et `Password` par 
       les informations de votre serveur SMTP.
     - Si vous utilisez un service de messagerie SendGrid, assurez-vous 
       de reemplacer `ApiKey` par la clé API fournie par le service.
---
3. **Configuration du frontend (AssistClub.UI.Blazor)** :
Dans le projet AssistClub.UI.Blazor, ouvrez le fichier `appsettings.json` 
et modifiez les champs de la section "Google" pour correspondre à votre configuration OAuth de Google :
     ```json
     "Google": {
       "ClientId": "your_client_id",
       "ClientSecret": "your_client_secret"
     }
     ```
     - Remplacez `ClientId` et `ClientSecret` par les informations fournies par Google.

### Lancer l'environnement localement
1. **Cloner le dépôt** : Clonez le dépôt GitHub sur votre machine locale.
   ```bash
   git clone https://github.com/cegepst/AssistClub.git
   cd AssistClub
   ```
---
2. **Lancer les conteneurs Docker** : 
   Assurez-vous que Docker est en cours d'exécution. 
   Dans le répertoire racine du projet, exécutez la commande suivante pour construire l'image Docker :
   ```bash
   docker compose up --build
   ```
   Cela démarre:
   - La base de données SQL Server (`assistclub-db`).
   - L'API .NET (`assistclub-api`).
   - L'initialisation de la base de données (`db-init`).
---
3. **Lancer le frontend Blazor** : Ouvrez un terminal dans la racine et exécutez la commande suivante :
   ```bash
   cd AssistClub.UI.Blazor
   dotnet run
   ```
   L'application sera accessible à l'adresse suivante : http://localhost:5192
   
### Redemarrer la base de données et l'API (au besoin)
Pour redémarrer la base de données et l'API, exécutez la commande suivante dans le répertoire racine du projet :
```bash
  docker compose down -v
  docker compose up --build
```

### Supprimer les conteneurs Docker
Pour arrêter et supprimer les conteneurs Docker, exécutez la commande suivante dans le répertoire racine du projet :
```bash
  docker compose down -v
```