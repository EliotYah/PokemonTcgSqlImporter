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

### Moving Classes Into Separate Files

I moved the PokemonCardData class out of Program.cs and into its own file called PokemonCardData.cs.

Important idea:
- Program.cs should focus on the main program flow
- PokemonCardData.cs should focus on the card data structure
- Separating classes into their own files makes the project easier to read and maintain
- This is closer to how real C# projects are usually organized

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


## Notes 7/8/2026

### Improved Cards Table Schema

I rebuilt the dbo.Cards table to use a more professional database design.

Changes made:
- Added RecId as the internal database primary key
- Made CardId unique so duplicate Pokemon cards cannot be inserted
- Added DateAdded so SQL Server records when each row was imported
- Changed some SQL data types to better match the meaning of the data

Current table design ideas:
- RecId is an INT IDENTITY column created by SQL Server
- CardId is a VARCHAR value from the Pokemon dataset, such as base1-1
- Name uses NVARCHAR because card names may contain special characters
- Hp uses INT because HP is a number and should sort like a number
- Level uses INT because level is also numeric when available
- Number uses VARCHAR because card numbers may contain letters or special formats
- Artist and EvolvesFrom use NVARCHAR because names may contain special characters
- DateAdded uses DATETIME2 with SYSUTCDATETIME() so SQL Server fills it automatically

Important idea:
The JSON source can store a value as a string, but the SQL database should store the value based on how I want to query and use it.

Example:
- JSON hp is a string like "80"
- C# reads hp as a string
- SQL stores Hp as INT so I can sort and compare HP correctly

### Updated C# Import Logic

I updated the C# INSERT statement to match the new schema.

Changes made:
- Do not insert RecId because SQL Server creates it automatically
- Do not insert DateAdded because SQL Server fills it automatically
- Use TextToDbValue for text columns
- Use NumberToDbValue for numeric columns like Hp and Level

TextToDbValue handles text fields:
- Empty or null strings become SQL NULL using DBNull.Value
- Normal text values are inserted as text

NumberToDbValue handles numeric fields:
- Empty or null strings become SQL NULL using DBNull.Value
- Valid number strings are converted into int values
- Invalid number strings become SQL NULL for now

Important idea:
The importer acts as a translator between the JSON format and the SQL table design.

### Index Notes

An index helps SQL Server find, filter, sort, or group rows faster. Indexes should not be added randomly. Each index should have a reason based on how the data is queried.

Possible indexes for this project:

CardId:
- CardId should be unique because it comes from the Pokemon dataset
- It prevents duplicate cards from being inserted
- It is useful for looking up one exact card
- SQL Server already creates a unique index because CardId has a UNIQUE constraint

Name:
- Name may be useful if users search for cards by name
- Example query: find cards where the name contains Charizard

Rarity:
- Rarity is useful because queries group and count cards by rarity
- Example query: count how many cards exist for each rarity

Supertype:
- Supertype is useful because queries group cards into Pokemon, Trainer, and Energy
- Example query: count cards by supertype

Important idea:
Indexes can make reads faster, but they can make inserts and updates slightly slower because SQL Server has to maintain the indexes. I should index columns that are commonly searched, filtered, grouped, joined, or required to be unique.
