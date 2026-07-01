USE [PokemonTCG]
GO

/****** Object:  Table [dbo].[Cards]    Script Date: 7/1/2026 1:01:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cards](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Hp] [nvarchar](20) NULL,
	[Number] [nvarchar](20) NULL,
	[Artist] [nvarchar](100) NULL,
	[Supertype] [nvarchar](50) NULL,
	[Rarity] [nvarchar](100) NULL,
	[Level] [nvarchar](20) NULL,
	[EvolvesFrom] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


