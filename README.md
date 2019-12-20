# Game of Drone Backend

## Summary
Game of Drone is a web version of the famous game `Rock-Papper-Scissor` where two players trying to conquer each other. The backend version was implemented using the modern technologies and libraries: `.Net Core`, `Entity Framework Core`, `Dapper` and `Sql Server` for the database structure. The main idea in the implementation was build a `N-Layer` architecture with 4 layer to handle the all bussines logic, the data flow from `Entry Point` layer to `Domain` layer and return in the opositive direction. `N-Layer` architecture accompanied by good Desing Patterns like `Repository Pattern`,`UnitOfWork` and `Dependency Injection` made the backend code of Game of Drone reusable, cleaner and scalable. This game also contain a frontend version implemented with `ReactJs` and you can find it [here](https://github.com/abelpenton/GOD-frontend.git).


# Setup
```console
>docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pass@123' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest

>dotnet build

>dotnet run

```

## GOD Architecture
In the `4-Layer` architecture of `Game of Drone` the data flow from one layer to other using `Dependency Injection` patterns. Read below to see each layer in the architecture.

### Domain Layer:
For the domain layer was used `DDD` priciple (OOP applied to business models). For a reusability code in Domain Layer two generic entities was implemented and each domain of the bussiness should implement this `Core` entities. Then in the `Models` folder we can find the models for Game of Drone game.

```
|_____GOD.Domain
|_______Core
|__________Entity.cs
|__________ITrackeableEntity.cs
|_______Models
|__________Game.cs
|__________Move.cs
|__________Player.cs
|__________Round.cs
```

### Data Access Layer:
This layer have the responsability of handle the data from the database, for that task `Entity Framework` and `Dapper` was used.
To model the database of `Game of Drone` we can see `Context.cs`, also we can find the `Repository Patter` for good desing practices and a better way to make database CRUD operations. Also a `Core` entities was implemented looking for reusability and scalable code.

```
|_____GOD.DataAccess
|_______Context
|_________GODDataContext.cs
|_______Repositories
|_________Core
|___________IBaseRepository.cs
|___________BaseRepository.cs
|_________GODRepositories
|___________Game
|______________IGameRepository.cs
|______________GameRepository.cs
|___________Player
|______________IPlayerRepository.cs
|______________PlayerRepository.cs
|___________Round
|______________IRoundRepository.cs
|______________RoundRepository.cs
|_________UnitOfWork
|___________IUnitOfWork.cs
|___________UnitOfWork.cs
```

### Bussines Layer:
This layer have the responsability of make all bussiness logic and then use the repositories to access of data, here is where `Unit of Work` was used, the idea was shared the `Context` for every service and make the database transaction after each CRUD operation was done by the repositories in the service. As well as the other layer a `Core` entities was implemented for generic services.

```
|_____GOD.BussineServices
|_______Core
|_________IBaseBussinesServices.cs
|_________BaseBussinesServices.cs
|_______Services
|_________Game
|___________IGameService.cs
|___________GameService.cs
|_________Player
|___________IPlayerService.cs
|___________PlayerService.cs
|_________Round
|___________IRoundService.cs
|___________RoundService.cs
```

### Entry Point Layer
This is the top of `N-Layer` architecture, here we can find the endpoint of `Game of Drone`, in this layer the request data are mapped then the services are called to get the bussines result depending of the input.


## New Feature
This is a guide add a new feature to the architecture. Keep in mind with this steps you only will be able to make standart operation for the `NewEntity`, if you will whish a new beheavior you will need implement by yourself.

### Domain Layer

>1- Add new bussines model and inherit from `Entity<TKey>` [Models](`/src/GOD.Domain/Models/`)

### Data Access Layer

>1- Add `DbSet<NewEntity>` to GODDataContext [Context](`/src/GOD.DataAccess/Context/`)

>2- Add `INewEntityRepository` and inherit from `IRepository<NewEntity<TKey>>` [IGODRepositories](`/src/GOD.DataAccess/Repositories/GODRepositories/`)

>3- Add `NewEntityRepository` and inherit from `Repository<NewEntity<TKey>>` and `INewEntityRepository` [GODRepositories](`/src/GOD.DataAccess/Repositories/GODRepositories/`)

>4- Use `Dapper` for specific operation in `NewEntityRepository`

>5- Register Dependency Injection for `INewEntityRepository` and `NewEntityRepository` in `Startup.cs`

### Bussines Layer

>1- Add `INewEntityService` and inherit from `IBaseBussineService<NewEntity<TKey>>` [IGODBussineServices](`/src/GOD.BussineServices/Services/`)

>2- Add `NewEntityService` and inherit from `BaseBussines<NewEntity<TKey>>` and `INewEntityService` [GODBussineServices](`/src/GOD.BussineServices/Services/`)

>3- Register Dependency Injection for `INewEntityService` and `NewEntityService` in `Startup.cs`

### Entry Point Layer

>1- Add `NewEntityDto` to mapper the request data with you `NewEntity` model [NewEntityDto](`./Models/`)

>2- Add `AutoMapper` configuration for `NewEntityDto` and `NewEntity` model [AutoMapper](`./Models/MapperConfig.cs`)

>3- Inject `INewService` in `GameController` constructor [Controller](`./Controllers/GameController.cs`)

>4- Add your new endpoints and use `INewService`. [EndPoint](`./Controllers/GameController.cs`)


## To Improve
>1- Build some `Unit Test` for the services(Though I config Swagger so you can test the current EndPoints https://localhost:5001/swagger/).

>2- Build a docker compose for a better setup.

>3- Use `ILogger` for track some operation or failures.

>4- Comment the code.