-- Sort by rarity
SELECT Rarity, COUNT(*) AS CardCount 
FROM dbo.Cards
GROUP BY Rarity
ORDER BY CardCount DESC;

-- check the missing null rarity --
SELECT CardId, Name, Rarity
FROM dbo.Cards
WHERE Rarity IS NULL;
-- seems to be that cards where their rarity is null are usually promos and energy cards --

-- Count how many cards are in the database
SELECT COUNT(*) AS [Number Of Cards]
FROM dbo.Cards;

-- Count cards by supertype
SELECT Supertype, COUNT(*) AS CardCount
FROM dbo.Cards
GROUP BY Supertype;

