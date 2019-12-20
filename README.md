# Game of Drone Backend

## Summary
Game of Drone is a web version of the famous game `Rock-Papper-Scissor` where two players try to conquer each other. The backend version is implemented using modern technologies and libraries like `.Net Core`, `Entity Framework Core`, `Dapper` and `Sql Server` for database structure. The main idea in the implementation is to build an `N-layer` architecture with 4 layers to handle the logic of the whole business, the data flows from the `Entry Point` layer to the `Domain` layer and return in the opposite direction. `N-Layer's` architecture accompanied by some design patterns such as `Repository Pattern`, `UnitOfWork` and `Dependency Injection` to make the Game of Drone code reusable, cleaner and scalable. This game also contains a user interface version implemented with `ReactJs` and it can be found [here](https://github.com/abelpenton/GOD-frontend.git).


# Setup
```console
>docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pass@123' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest

>dotnet build

>dotnet run

```

## GOD's Architecture
In the `4-layer` architecture of `Game of Drone`, data flows from one layer to another using the `Dependency Injection` pattern. Below each layer is explained.

### Domain Layer:
The `DDD` principle (OOP applied to business models) was used for the domain layer. To reuse the code in the domain layer, two generic entities are implemented and each business domain implements them. Then, in the `Models` folder, you can find the models for the Game of Drone game.


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
This layer has the responsibility of handling the data, for that task `Entity Framework` and `Dapper` are used. The model of the `Game of Drone` database can be found in `Context.cs`. You can find in the `Repository` implementation good design practices and a good way to do CRUD operations on the database. A `Core` entity was also implemented for reuse and scalable code.

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
This layer has the responsibility of making all the business logic and then using the repositories to access the data, this is where the `Unit of Work` is used, the idea of the `Context` is to be shared to each service and to make a transaction to the database after a CRUD operation is performed by the repositories in the service. In addition to the other layer, central entities for generic services are implemented.

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

### Entry Point Layer:
This is the top of the `N-Layer` architecture, here you can find the `Game of Drone` endpoint, in this layer the application data is assigned and then the services are called to obtain the business results depending on the input.


## New Feature
This is a guide to add new features to the architecture. Note that with these steps you can only perform a standard operation for `NewEntity`, if you want a new behavior you will need to implement it yourself.


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
>1- Build some Unit Test for the services(Swagger was configured to test the current EndPoints https://localhost:5001/swagger/).

>2- Build a docker compose for a better setup.

>3- Use `ILogger` for track some operation or failures.

>4- Comment the code.