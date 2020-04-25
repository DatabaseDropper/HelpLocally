# Projekt Help locally

Domyślne poświadczenia do testów: admin/12345

## Baza danych (docker)

W folderze `HelpLocally` (z plikiem `HelpLocally.sln` i `docker-compose.dev.yml`)
    
    docker-compose -f docker-compose.dev.yml up -d

## Migracje

W folderze `HelpLocally/HelpLocally.Infrastructure`

### Generowanie migracji

    dotnet ef migrations add NazwaMigracji -s ../HelpLocally.Web/

### Update bazy danych 

    dotnet ef database update -s ../HelpLocally.Web/
