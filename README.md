## Autor

Juan David Jimenez

## Descripcion

This project is a TeamsApp with oriented architecture to microservices and practices of developent with architecture capes. Project const of 3 microservices, 1 TeamApi, 2 MemberApi, 3 ManagerApi. Proposite of this project is to create a team with members and manager, and manager can create a meeting with members of team.

## Usage

```bash
Running project with 3 microservices
dotnet run
```

## Dependencies libraries with version used

```bash
   Project 'Teams.ApiTeam.Service' has the following package references
   [net7.0]:
   Top-level Package                    Requested   Resolved
   > AutoMapper                         12.0.1      12.0.1
   > FluentValidation                   11.7.1      11.7.1
   > FluentValidation.AspNetCore        11.3.0      11.3.0
   > Microsoft.AspNetCore.OpenApi       7.0.11      7.0.11
   > Microsoft.EntityFrameworkCore      7.0.11      7.0.11
   > MySql.EntityFrameworkCore          7.0.5       7.0.5
   > Newtonsoft.Json                    13.0.3      13.0.3
   > RestSharp                          110.2.0     110.2.0
   > Swashbuckle.AspNetCore             6.5.0       6.5.0

   Project 'Teams.ApiMember.Service' has the following package references
   [net7.0]:
   Top-level Package                    Requested   Resolved
   > AutoMapper                         12.0.1      12.0.1
   > FluentValidation.AspNetCore        11.3.0      11.3.0
   > Microsoft.AspNetCore.OpenApi       7.0.11      7.0.11
   > Microsoft.EntityFrameworkCore      7.0.11      7.0.11
   > MySql.EntityFrameworkCore          7.0.5       7.0.5
   > Newtonsoft.Json                    13.0.3      13.0.3
   > RestSharp                          110.2.0     110.2.0
   > Swashbuckle.AspNetCore             6.5.0       6.5.0

   Project 'Teams.ApiManager' has the following package references
   [net7.0]:
   Top-level Package                   Requested   Resolved
   > Microsoft.AspNetCore.OpenApi      7.0.11      7.0.11
   > Newtonsoft.Json                   13.0.3      13.0.3
   > RestSharp                         110.1.0     110.1.0
   > Swashbuckle.AspNetCore            6.5.0       6.5.0
```

## Scrips of database in mysql execute in terminal

```bash
source  /home/thenowrock/RiderProjects/TeamsApp/Scripts/DbTeam
source  /home/thenowrock/RiderProjects/TeamsApp/Scripts/DbTeamMember
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
