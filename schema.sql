USE PokemonTCG;
GO

CREATE TABLE dbo.Cards
(
    RecId INT IDENTITY(1,1) PRIMARY KEY,
    CardId VARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    Hp INT NULL,
    Number VARCHAR(20) NULL,
    Artist NVARCHAR(200) NULL,
    Supertype VARCHAR(50) NULL,
    Rarity VARCHAR(100) NULL,
    Level INT NULL,
    EvolvesFrom NVARCHAR(200) NULL,
    DateAdded DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

CREATE TABLE dbo.CardTypes
(
    RecId INT IDENTITY(1,1) PRIMARY KEY,
    CardRecId INT NOT NULL,
    TypeName VARCHAR(50) NOT NULL,
    FOREIGN KEY (CardRecId) REFERENCES dbo.Cards(RecId)
);
GO

CREATE INDEX IX_Cards_Rarity
ON dbo.Cards (Rarity);
GO

CREATE INDEX IX_Cards_Supertype
ON dbo.Cards (Supertype);
GO
