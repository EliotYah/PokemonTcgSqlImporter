-- Sort by rarity
SELECT Rarity, COUNT(*) AS CardCount
FROM dbo.Cards
GROUP BY Rarity
ORDER BY CardCount DESC;

-- Find cards missing a rarity value
SELECT CardId, Name, Rarity
FROM dbo.Cards
WHERE Rarity IS NULL;

-- Cards where rarity is null are usually promos and energy cards

-- Count how many cards are in the database
SELECT COUNT(*) AS [Number Of Cards]
FROM dbo.Cards;

-- Count cards by supertype
SELECT Supertype, COUNT(*) AS CardCount
FROM dbo.Cards
GROUP BY Supertype;

-- Show cards with their types
SELECT
    c.CardId,
    c.Name,
    ct.TypeName
FROM dbo.Cards AS c
INNER JOIN dbo.CardTypes AS ct
    ON c.RecId = ct.CardRecId;
