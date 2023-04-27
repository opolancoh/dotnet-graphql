# DotNet Core & GraphQL
A brief example of working with DotNet Core and GraphQL. In this example, we are going to create a Web API to request some data.

### Technologies in this repo:
* DotNet Core 6
* Entity Framework 6 (Code First)
* Postgres (Docker Container)
* HotChocolate (GraphQL)

## Database
We are using Postgres as the default database, but you can use any Entity Framework supported database.

#### Setup Database
Create the database container (you need to have Docker installed on your system):

```sh
docker run -d --name dotnet-graphql-postgres -p 5432:5432 -e POSTGRES_PASSWORD=My@Passw0rd postgres
```

Stop and remove the container when needed:

```sh
docker stop dotnet-graphql-postgres && docker rm dotnet-graphql-postgres
```

#### Create Database

Apply the existing migration (run this command in the application root folder):

```sh
dotnet ef database update --project DotNetGraphQL.Web
```

#### Add Migration

```sh
dotnet ef migrations add "InitialMigration" --project DotNetGraphQL.Web
```

#### Data Seed

To add initial data just uncomment this line in Program.cs:

```cs
// DataHelper.Seed(app);
```
