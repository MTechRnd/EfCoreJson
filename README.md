# EfCoreJson
EfCore Support of Json

You need to just run update-database command when you clone this repo as following:
- Update-Database -context DataContext
- Update-Database -context JsonDataContext

Following are basic command which i have used:
- Add-Migration "migration for traditional schema" -context DataContext
- Add-Migration "migration for json column schema" -context JsonDataContext
- Update-Database -context DataContext
- Update-Database -context JsonDataContext
