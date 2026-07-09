USE [PokemonTCG]
GO

/****** Object:  Table [dbo].[Cards]    Script Date: 7/8/2026 4:54:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cards](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[CardId] [varchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Hp] [int] NULL,
	[Number] [varchar](20) NULL,
	[Artist] [nvarchar](200) NULL,
	[Supertype] [varchar](50) NULL,
	[Rarity] [varchar](100) NULL,
	[Level] [int] NULL,
	[EvolvesFrom] [nvarchar](200) NULL,
	[DateAdded] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cards] ADD  DEFAULT (sysutcdatetime()) FOR [DateAdded]
GO


