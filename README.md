# Task Manager

---

## Wymagania wstępne
Przed rozpoczęciem instalacji upewnij się, że masz zainstalowane następujące oprogramowanie:
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (np. SQL Server Express)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

---

## Instalacja

Wykonaj poniższe kroki, aby uruchomić projekt lokalnie.

1. **Sklonuj repozytorium**
   ```bash
   git clone https://github.com/taczhed/project-task-manager.git
   cd project-task-manager

2. **Przywróć zależności Uruchom poniższą komendę, aby zainstalować wymagane pakiety NuGet**
   ```bash
   dotnet restore

3. **Zaktualizuj plik appsettings.json, podając odpowiedni connection string do bazy danych (zmień nazwę serwera):**
   ```json
   ...
   "ConnectionStrings": {
     "ApplicationDbContextConnection": "Server=TACZHED;Database=project-task-manager;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
   }
   ...

4. **Zastosuj migracje. Projekt korzysta z Entity Framework Core do zarządzania bazą danych. Zastosuj migracje, aby utworzyć schemat bazy danych, z poziomu Package Manager Console, użyj polecenia::**
   ```bash
   update-database

5. **Uruchom projekt Uruchom serwer deweloperski, aplikacja będzie dostępna pod adresem https://localhost:5001 (lub na porcie zdefiniowanym w pliku launchSettings.json):**
   ```bash
   dotnet run