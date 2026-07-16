# Pokemon TCG SQL Importer

A C# console application that reads Pokemon TCG card data from JSON files and imports the data into Microsoft SQL Server.

## Project Goal

The goal of this project is to practice C#, JSON parsing, SQL Server, and basic ETL/database design concepts.

ETL means:
- Extract: read JSON files from a folder
- Transform: convert JSON data into C# objects and prepare values for SQL Server
- Load: insert the data into a SQL Server table

## Technologies Used

- C#
- .NET
- Microsoft SQL Server
- SQL Server Management Studio
- Microsoft.Data.SqlClient
- System.Text.Json

## What The Program Does

- Finds all JSON files in the local Pokemon TCG cards folder
- Reads each JSON file
- Deserializes card data into C# objects
- Combines all cards into one list
- Connects to SQL Server
- Inserts card records into the dbo.Cards table
- Converts missing C# values into SQL NULL values
- Converts numeric text values like HP and Level into SQL integer values

## Database

The main table is dbo.Cards.

The table uses:
- RecId as the internal SQL Server primary key
- CardId as the unique Pokemon dataset card id
- DateAdded to track when each row was inserted
- INT columns for numeric values such as Hp and Level
- VARCHAR/NVARCHAR columns depending on the kind of text being stored

The table creation script is included in schema.sql.

## SQL Queries

Sample analysis queries are included in queries.sql.

The queries include:
- Counting total cards
- Grouping cards by rarity
- Finding cards with missing rarity values
- Grouping cards by supertype

## Dataset

This project uses JSON card data from the PokemonTCG/pokemon-tcg-data repository:

https://github.com/PokemonTCG/pokemon-tcg-data

The dataset is not included in this repository. To run this project, download or clone the dataset locally and update the folderPath variable in Program.cs.

## How To Run

1. Download or clone the Pokemon TCG dataset.
2. Create a SQL Server database named PokemonTCG.
3. Run schema.sql in SQL Server Management Studio to create the dbo.Cards table.
4. Update the folderPath variable in Program.cs to point to your local cards/en folder.
5. Run the C# console application.
6. Use queries.sql to analyze the imported data.

## What I Learned

- How to read files from a folder in C#
- How to deserialize JSON into C# classes
- How to connect a C# program to SQL Server
- How SQL INSERT statements work
- How parameterized queries help safely insert data
- How to handle null values using DBNull.Value
- How to convert string values into SQL numeric values
- Why a database can use RecId as an internal primary key while keeping CardId unique
- How to write basic SQL queries using SELECT, WHERE, GROUP BY, COUNT, and ORDER BY
- Why indexes should be added intentionally based on how data is queried
