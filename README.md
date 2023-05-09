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

#### Example #1 (Get by ID)

Operations
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

#### Example #2 (Get by ID)

Operations
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

#### Example #3 (Get by ID)

Operations
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

#### Example #4 (Get all - Projection - Filtering - Sorting)

Operations
```js
query {
    books(
        order: [{ title: ASC }]
    where: { or: [{ title: { contains: "0" } }] }
) {
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
    "books": [
      {
        "id": "72d95bfd-1dac-4bc2-adc1-f28fd43777fd",
        "title": "Book 01",
        "publishedOn": "2023-04-27T04:36:10.862Z",
        "reviews": [
          {
            "id": "06162506-fca5-40a9-996e-9a93ca01a4f0",
            "comment": "Comment 01_03",
            "rating": 1
          },
          {
            "id": "b1ab0fe9-5b0e-4b4a-8ea5-6c3007fde4cd",
            "comment": "Comment 01_02",
            "rating": 3
          },
          {
            "id": "e66e7a9c-164d-44cf-8092-8a09eb39c62a",
            "comment": "Comment 01_01",
            "rating": 5
          }
        ]
      },
      {
        "id": "c32cc263-a7af-4fbd-99a0-aceb57c91f6b",
        "title": "Book 02",
        "publishedOn": "2023-04-27T04:36:10.862Z",
        "reviews": [
          {
            "id": "0f4d70d5-e719-4f01-bde5-6d04fb0f7837",
            "comment": "Comment 02_02",
            "rating": 2
          },
          {
            "id": "f5d55800-6518-40dd-b18d-e4522b54d32d",
            "comment": "Comment 02_01",
            "rating": 4
          }
        ]
      },
      {
        "id": "7b6bf2e3-5d91-4e75-b62f-7357079acc51",
        "title": "Book 03",
        "publishedOn": "2023-04-27T04:36:10.862Z",
        "reviews": [
          {
            "id": "3c9cebcf-ae63-4ae6-af62-fdea8d5e0302",
            "comment": "Comment 03_01",
            "rating": 3
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
WHERE (@__p_0 = '') OR (strpos(b."Title", @__p_0) > 0)
ORDER BY b."Title", b."Id"
```

### Mutations

#### Example #1 (Create)

Operations
```js
mutation ($item: BookForCreatingDtoInput!) {
    createBook(item: $item) {
        id
    }
}
```
Variables
```json
{
  "item": {
    "title": "My Book",
    "publishedOn": "2022-09-28T00:00:00Z"
  }
}
```
Response
```json
{
  "data": {
    "createBook": {
      "id": "7f7efa0a-3f38-4393-a807-f93253864749"
    }
  }
}
```
SQL generated code
``` sql
INSERT INTO "Books" ("Id", "PublishedOn", "Title")
VALUES (@p0, @p1, @p2);
```

#### Example #2 (Update)

Operations
```js
mutation ($item: BookForUpdatingDtoInput!) {
    updateBook(item: $item) {
        id,
            title,
            publishedOn
    }
}
```
Variables
```json
{
  "item": {
    "id": "b60121ed-af91-4b78-9bd7-f8803e9c24dd",
    "title": "My Book 2",
    "publishedOn": "2023-10-28T00:00:00Z"
  }
}
```
Response
```json
{
  "data": {
    "updateBook": {
      "id": "b60121ed-af91-4b78-9bd7-f8803e9c24dd",
      "title": "My Book 2",
      "publishedOn": "2023-10-28T00:00:00.000Z"
    }
  }
}
```
SQL generated code
``` sql
UPDATE "Books" SET "PublishedOn" = @p0, "Title" = @p1
WHERE "Id" = @p2;
```

#### Example #3 (Delete)

Operations
```js
mutation ($id: UUID!) {
    deleteBook(id: $id)
}
```
Variables
```json
{
  "id": "7f7efa0a-3f38-4393-a807-f93253864749"
}
```
Response
```json
{
  "data": {
    "deleteBook": "Item with id '$7f7efa0a-3f38-4393-a807-f93253864749' was deleted!"
  }
}
```
SQL generated code
``` sql
DELETE FROM "Books"
WHERE "Id" = @p0;
```