# Pokemon TCG SQL Importer

A C# console application that reads Pokemon TCG card data from JSON files and imports the data into Microsoft SQL Server.

## Project Goal

The goal of this project was to practice working with C#, JSON data, SQL Server, and basic ETL concepts.

ETL means:
- Extract: read JSON files
- Transform: convert JSON data into C# objects and handle missing values
- Load: insert the data into a SQL Server table

## Technologies Used

- C#
- .NET
- Microsoft SQL Server
- SQL Server Management Studio
- Microsoft.Data.SqlClient
- System.Text.Json

## What The Program Does

- Finds all JSON files in the local cards folder
- Reads each JSON file
- Deserializes card data into C# objects
- Combines all cards into one list
- Connects to SQL Server
- Inserts card records into the dbo.Cards table
- Converts missing C# values into SQL NULL values

## Database

The main table is dbo.Cards.

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

The dataset is not included in this repository. To run the project, download or clone the dataset locally and update the folderPath variable in Program.cs.

## What I Learned

- How to read files from a folder in C#
- How to deserialize JSON into C# classes
- How to connect a C# program to SQL Server
- How SQL INSERT statements work
- How parameterized queries help safely insert data
- How to handle null values when inserting into SQL Server
- How to write basic SQL queries using SELECT, WHERE, GROUP BY, COUNT, and ORDER BY