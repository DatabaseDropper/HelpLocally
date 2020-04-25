# Projekt Help locally

Domyślne poświadczenia do testów: admin/12345

![img1](https://i.imgur.com/HtuRmHL.png)
![img2](https://i.imgur.com/dZJoASV.png)
![img3](https://i.imgur.com/4I7s6u0.png)
![img4](https://i.imgur.com/2xY6z3i.png)
![img5](https://i.imgur.com/Uv2ugvO.png)
![img6](https://i.imgur.com/VsFOmlz.png)
![img7](https://i.imgur.com/dk79wmS.png)

## Baza danych (docker)

W folderze `HelpLocally` (z plikiem `HelpLocally.sln` i `docker-compose.dev.yml`)
    
    docker-compose -f docker-compose.dev.yml up -d

## Migracje

W folderze `HelpLocally/HelpLocally.Infrastructure`

### Generowanie migracji

    dotnet ef migrations add NazwaMigracji -s ../HelpLocally.Web/

### Update bazy danych 

    dotnet ef database update -s ../HelpLocally.Web/
