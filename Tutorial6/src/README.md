# Animal clinic system
This is a management system for an animal clinic that allows you to manage animals and visits.
### Project structure
- Controllers: contains API controller.
- DTO: contains DTOs, needed for passing data.
- Helpers: contains DBContext file.
- Migrations: contains auto-generated migration files.
- Models: contains data models for Animal, AnimalTypes, Employee and Visit.
- Scripts: contains file with sample data for insertion.
### Features
Actions possible for both animals and visits:
- Get all,
- Get by Id,
- Update,
- Add,
- Delete.
### Dependencies
- Microsoft.EntityFrameworkCore.SqlServer
- .NET 8.0 SDK
- - Swagger
- Dockerized MsSql database
### Set up instructions
1. Clone git repository.
2. Check dependencies and install them if needed.
3. Configure your connection string.
4. Insert sample data into database.
5. Run application.
