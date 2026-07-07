# Project Notes

## Project Summary

This project is a C# console application that imports Pokemon TCG JSON card data into Microsoft SQL Server.

The project started as practice for learning C#, SQL Server, JSON parsing, and basic ETL. It now imports all card JSON files from the local dataset folder, converts the data into C# objects, handles missing values, and inserts records into a SQL Server table.

ETL means:
- Extract: read JSON files from a folder
- Transform: deserialize JSON into C# objects and handle missing values
- Load: insert the cleaned data into SQL Server

## Dataset

The dataset used is from the PokemonTCG/pokemon-tcg-data repository:

https://github.com/PokemonTCG/pokemon-tcg-data

The dataset is not included in this project repository. The program expects the dataset to exist locally, and the folder path is set in Program.cs.

## C# Concepts Practiced

### Reading Files

I learned how to read JSON files from a folder using Directory.GetFiles.

Important idea:
- folderPath points to the folder
- jsonFiles stores all matching JSON file paths
- foreach loops through each file

### StreamReader

I used StreamReader to open and read each JSON file.

Important idea:
- StreamReader opens the file
- ReadToEnd reads the full file contents into a string

### JSON Deserialization

I used System.Text.Json to turn JSON text into C# objects.

Important idea:
- JSON properties need to match the C# class property names
- Arrays in JSON become List<T> in C#
- A JSON file containing many cards becomes List<PokemonCardData>

### Classes

I created a PokemonCardData class to represent card data from the JSON files.

The class includes fields such as:
- id
- name
- hp
- number
- artist
- supertype
- rarity
- level
- evolvesFrom
- types
- subtypes

### Lists

I learned that JSON arrays should be represented as lists in C#.

Example:

```json
"types": ["Psychic"]
```

This becomes:

```csharp
public List<string> types { get; set; }
```

### Combining Data

I created one main list, allCardList, to hold cards from every JSON file.

Important idea:
- Each JSON file has its own list of cards
- AddRange adds those cards into one combined list
- This made it possible to insert all cards into SQL Server later

## SQL Server Concepts Practiced

### Creating A Database Table

I created a dbo.Cards table in SQL Server Management Studio.

The table stores basic card fields such as:
- Id
- Name
- Hp
- Number
- Artist
- Supertype
- Rarity
- Level
- EvolvesFrom

### Primary Key

The first version of the table used the Pokemon card id as the primary key.

Example:

```text
base1-1
base1-2
```

Primary key means the column uniquely identifies each row and can not have a null or duplciate value.

This worked, but I later learned that many companies prefer using a separate internal database id called RecId.(Will tackle this tomorrow)

### NULL Values

I learned that C# null and SQL NULL are not the same thing.

When inserting missing values into SQL Server, I used DBNull.Value.

Important idea:
- C# null means no object/value in C#
- SQL NULL means missing/unknown value in the database
- SQL parameters need DBNull.Value when inserting SQL NULL

### Parameterized Queries

I used parameterized SQL INSERT statements instead of building SQL strings manually.

Important idea:
- Parameters are placeholders like @Name and @Hp
- C# fills those placeholders safely
- This is safer and cleaner than string concatenation

### ExecuteNonQuery

I used ExecuteNonQuery to run SQL INSERT commands.

Important idea:
- Use ExecuteNonQuery for INSERT, UPDATE, and DELETE
- Use SELECT queries in SSMS to verify inserted data

## SQL Queries Practiced

I created queries.sql to save useful SQL queries.

Queries practiced:
- Count total cards
- Count cards by rarity
- Find cards with missing rarity values
- Count cards by supertype

Important SQL concepts:
- SELECT chooses columns
- FROM chooses the table
- WHERE filters rows
- IS NULL checks for missing values
- COUNT counts rows
- GROUP BY groups similar values
- ORDER BY sorts results


## Notes 7/1/2026

## RecId Learning

I learned about the idea of using a RecId.

Current idea:
- CardId should store the Pokemon dataset id, such as base1-1
- RecId should be an internal SQL Server identity column

Example future table design:

```text
RecId | CardId  | Name
1     | base1-1 | Alakazam
2     | base1-2 | Blastoise
```

A possible SQL definition:

```sql
RecId INT IDENTITY(1,1) PRIMARY KEY,
CardId NVARCHAR(50) NOT NULL UNIQUE
```
IDENTITY(1,1) means SQL Server will automatically create a new RecId for each inserted row, starting at 1 and incrementing by 1.
NOT NULL UNIQUE means CardId must have a value and cannot be duplicated.

Important idea:
- RecId is created by SQL Server automatically
- CardId comes from the JSON dataset
- RecId is useful for joins and internal database relationships
- CardId should still be unique so duplicate cards are not inserted
