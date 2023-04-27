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
## GraphQL

### Queries

#### Example #1

Query
```js
query {
    book(id: "72d95bfd-1dac-4bc2-adc1-f28fd43777fd") {
        id
        title
        publishedOn
        reviews {
            id
            comment
            rating
        }
    }
}

```

Response
```json
{
  "data": {
    "book": [
      {
        "id": "72d95bfd-1dac-4bc2-adc1-f28fd43777fd",
        "title": "Book 01",
        "publishedOn": "2023-04-27T04:13:45.392Z",
        "reviews": [
          {
            "id": "d12855e8-4dbd-492e-925d-a1d1e0f35bd1",
            "comment": "Comment 01_01",
            "rating": 5
          },
          {
            "id": "87f43452-1d31-4ccc-aa65-492470515748",
            "comment": "Comment 01_02",
            "rating": 3
          },
          {
            "id": "2933b0b5-85a4-49cd-88dd-fc4d4a8e18c3",
            "comment": "Comment 01_03",
            "rating": 1
          }
        ]
      }
    ]
  }
}
```

SQL generated code
``` sql
SELECT b."Id", b."Title", b."PublishedOn", r."Id", r."Comment", r."Rating"
FROM "Books" AS b
LEFT JOIN "Reviews" AS r ON b."Id" = r."BookId"
WHERE b."Id" = @__id_0
ORDER BY b."Id"
```

#### Example #2

Query
```js
query {
    book(id: "72d95bfd-1dac-4bc2-adc1-f28fd43777fd") {
        id
        reviews {
            comment
            rating
        }
    }
}

```

Response
```json
{
  "data": {
    "book": [
      {
        "id": "72d95bfd-1dac-4bc2-adc1-f28fd43777fd",
        "reviews": [
          {
            "comment": "Comment 01_03",
            "rating": 1
          },
          {
            "comment": "Comment 01_02",
            "rating": 3
          },
          {
            "comment": "Comment 01_01",
            "rating": 5
          }
        ]
      }
    ]
  }
}
```

SQL generated code
``` sql
SELECT b."Id", r."Comment", r."Rating", r."Id"
FROM "Books" AS b
LEFT JOIN "Reviews" AS r ON b."Id" = r."BookId"
WHERE b."Id" = @__id_0
ORDER BY b."Id"
```

#### Example #3

Query
```js
query {
    book(id: "72d95bfd-1dac-4bc2-adc1-f28fd43777fd") {
        id
    }
}
```

Response
```json
{
  "data": {
    "book": [
      {
        "id": "72d95bfd-1dac-4bc2-adc1-f28fd43777fd"
      }
    ]
  }
}
```

SQL generated code
``` sql
SELECT b."Id"
FROM "Books" AS b
WHERE b."Id" = @__id_0
```