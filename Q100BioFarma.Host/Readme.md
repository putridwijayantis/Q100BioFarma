## Clean Architecture Net Core 8

- Application Modular dependencies

## Technology

- .Net 8
- Swagger
- AutoMapper

To start developing your application:

```bash
dotnet run
```

## Database

Because the migration has not yet run, you can create a table with the following query

```bigquery
create table if not exists recipes
(
    id uuid default gen_random_uuid() not null
        constraint "PK_recipes"
        primary key,
    created_by uuid,
    created_at timestamp with time zone,
    updated_by uuid,
    updated_at timestamp with time zone,
    deleted_by uuid,
    deleted_at timestamp with time zone,
    name varchar(255) not null,
    description text
);

create table if not exists steps
(
    id uuid default gen_random_uuid() not null
        constraint "PK_steps"
        primary key,
    created_by uuid,
    created_at timestamp with time zone,
    updated_by uuid,
    updated_at timestamp with time zone,
    deleted_by uuid,
    deleted_at timestamp with time zone,
    name varchar(255) not null,
    recipe_id uuid constraint "FK_STEPS_1"
        references recipes
        on delete set null,
    ordering   int
);

create table if not exists sub_steps
(
    id uuid default gen_random_uuid() not null
        constraint "PK_sub_steps"
        primary key,
    created_by uuid,
    created_at timestamp with time zone,
    updated_by uuid,
    updated_at timestamp with time zone,
    deleted_by uuid,
    deleted_at timestamp with time zone,
    name varchar(255) not null,
    step_id uuid constraint "FK_SUB_STEPS_1"
        references steps
        on delete set null,
    ordering   int
);

create table if not exists parameters
(
    id uuid default gen_random_uuid() not null
        constraint "PK_parameters"
        primary key,
    created_by uuid,
    created_at timestamp with time zone,
    updated_by uuid,
    updated_at timestamp with time zone,
    deleted_by uuid,
    deleted_at timestamp with time zone,
    name varchar(255) not null,
    type varchar(255),
    step_id uuid constraint "FK_PARAMETERS_1"
        references steps
        on delete set null,
    description text
);
```

## Swagger
You can access swagger at:
```html
http://localhost:5000/sys/be/swagger/index.html
```

## Explanation API:
1. Recipes
```html
-- (GET) api/recipes --> get all recipes data
-- (POST) api/recipes --> add recipes data
-- (GET) api/recipes/{id} --> get detail recipes
-- (PUT) api/recipes/{id} --> edit recipes data
-- (DELETE) api/recipes/{id} --> remove recipes data
```
2. Steps
```html
-- (POST) api/steps/{recipeId} --> add step to recipe
-- (POST) api/steps/{stepId}/sub-step --> add sub step
-- (POST) api/steps/{stepId}/parameter --> add parameter to step
```