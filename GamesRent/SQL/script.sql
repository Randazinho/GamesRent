GO

CREATE DATABASE [GamesDB]
GO
ALTER DATABASE [GamesDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GamesDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GamesDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GamesDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GamesDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GamesDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GamesDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [GamesDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GamesDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GamesDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GamesDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GamesDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GamesDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GamesDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GamesDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GamesDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GamesDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GamesDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GamesDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GamesDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GamesDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GamesDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GamesDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GamesDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GamesDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GamesDB] SET  MULTI_USER 
GO
ALTER DATABASE [GamesDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GamesDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GamesDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GamesDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GamesDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GamesDB] SET QUERY_STORE = OFF
GO
USE [GamesDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [GamesDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Id_admin] [int] NOT NULL,
	[User_id] [int] NOT NULL,
	[today] [varchar] (50) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id_admin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id_booking] [int] IDENTITY(1,1) NOT NULL,
	[BookingDate] [date] NOT NULL,
	[Player_id] [int] NOT NULL,
	[VideoGame_id] [int] NOT NULL,
	[Week] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_booking] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Copy](
	[Id_copy] [int] IDENTITY(1,1) NOT NULL,
	[VideoGame_id] [int] NOT NULL,
	[Player_owner_id] [int] NOT NULL,
	[Available] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_copy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[Id_game] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreditCost] [int] NOT NULL,
	[Console] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_game] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loan](
	[Id_loan] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Ongoing] [int] NOT NULL,
	[Player_borrower_id] [int] NOT NULL,
	[Copy_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_loan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[Id_player] [int] NOT NULL,
	[Credit] [int] NOT NULL,
	[Pseudo] [varchar](50) NOT NULL,
	[RegistrationDate] [date] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[User_id] [int] NOT NULL,
	[Rating] [float] NOT NULL,
	[NbrRater] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_player] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id_user] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Admin] ([Id_admin], [User_id],[today]) VALUES (1, 1,N'Date')

GO
SET IDENTITY_INSERT [dbo].[Copy] ON 

INSERT [dbo].[Copy] ([Id_copy], [VideoGame_id], [Player_owner_id],[Available]) VALUES (2003, 1, 3,N'YES')
INSERT [dbo].[Copy] ([Id_copy], [VideoGame_id], [Player_owner_id],[Available]) VALUES (2004, 4, 2,N'YES')
SET IDENTITY_INSERT [dbo].[Copy] OFF

GO
SET IDENTITY_INSERT [dbo].[Game] ON 

INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (1, N'FIFA 23', 3, N'PLAYSTATION 5')
INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (3, N'Mario Kart', 1, N'SWITCH')
INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (4, N'COD Modern Warfare', 3, N'XBOX SERIES')
INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (5, N'Pokemon Violet', 3, N'SWITCH')
INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (6, N'GRAN TURISMO 6', 1, N'PLAYSTATION 3')
INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (7, N'HALO REACH', 1, N'XBOX 360')
INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (8, N'SPIDERMAN MORALES', 2, N'PLAYSTATION 4')
INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (9, N'HALO 5', 1, N'XBOX ONE')
INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (10, N'WII PARTY', 1, N'WII')
INSERT [dbo].[Game] ([Id_game], [Name], [CreditCost], [Console]) VALUES (11, N'Pokemon Perle', 1, N'DS')

SET IDENTITY_INSERT [dbo].[Game] OFF

GO
INSERT [dbo].[Player] ([Id_player], [Credit], [Pseudo], [RegistrationDate], [DateOfBirth], [User_id], [Rating], [NbrRater]) VALUES (2, 10, N'randaz', CAST(N'2023-08-06' AS Date), CAST(N'1998-10-15' AS Date), 2, 0, 0)
INSERT [dbo].[Player] ([Id_player], [Credit], [Pseudo], [RegistrationDate], [DateOfBirth], [User_id], [Rating], [NbrRater]) VALUES (3, 12, N'emra', CAST(N'2023-08-06' AS Date), CAST(N'1998-06-09' AS Date), 3, 0, 0)
INSERT [dbo].[Player] ([Id_player], [Credit], [Pseudo], [RegistrationDate], [DateOfBirth], [User_id], [Rating], [NbrRater]) VALUES (4, 0, N'poor', CAST(N'2023-08-06' AS Date), CAST(N'2000-12-31' AS Date), 4, 0, 0)
INSERT [dbo].[Player] ([Id_player], [Credit], [Pseudo], [RegistrationDate], [DateOfBirth], [User_id], [Rating], [NbrRater]) VALUES (5, 999, N'rich', CAST(N'2023-08-06' AS Date), CAST(N'2002-04-22' AS Date), 5, 0, 0)
INSERT [dbo].[Player] ([Id_player], [Credit], [Pseudo], [RegistrationDate], [DateOfBirth], [User_id], [Rating], [NbrRater]) VALUES (6, 12, N'messi', CAST(N'2023-08-06' AS Date), CAST(N'1987-01-06' AS Date), 6, 0, 0)
INSERT [dbo].[Player] ([Id_player], [Credit], [Pseudo], [RegistrationDate], [DateOfBirth], [User_id], [Rating], [NbrRater]) VALUES (7, 12, N'ronaldo', CAST(N'2023-08-06' AS Date), CAST(N'1985-01-06' AS Date), 7, 0,0)
INSERT [dbo].[Player] ([Id_player], [Credit], [Pseudo], [RegistrationDate], [DateOfBirth], [User_id], [Rating], [NbrRater]) VALUES (8, 12, N'baggio', CAST(N'2023-08-06' AS Date), CAST(N'1967-02-18' AS Date), 8, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id_user], [Username], [Password]) VALUES (1, N'admin', N'admin')
INSERT [dbo].[User] ([Id_user], [Username], [Password]) VALUES (2, N'randaz', N'randaz')
INSERT [dbo].[User] ([Id_user], [Username], [Password]) VALUES (3, N'emra', N'emra')
INSERT [dbo].[User] ([Id_user], [Username], [Password]) VALUES (4, N'poor', N'poor')
INSERT [dbo].[User] ([Id_user], [Username], [Password]) VALUES (5, N'rich', N'rich')
INSERT [dbo].[User] ([Id_user], [Username], [Password]) VALUES (6, N'messi', N'messi')
INSERT [dbo].[User] ([Id_user], [Username], [Password]) VALUES (7, N'ronaldo', N'ronaldo')
INSERT [dbo].[User] ([Id_user], [Username], [Password]) VALUES (8, N'baggio', N'baggio')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_User] FOREIGN KEY([User_id])
REFERENCES [dbo].[User] ([Id_user])
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_User]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Game] FOREIGN KEY([VideoGame_id])
REFERENCES [dbo].[Game] ([Id_game])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Game]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Player] FOREIGN KEY([Player_id])
REFERENCES [dbo].[Player] ([Id_player])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Player]
GO
ALTER TABLE [dbo].[Copy]  WITH CHECK ADD  CONSTRAINT [FK_Copy_Game] FOREIGN KEY([VideoGame_id])
REFERENCES [dbo].[Game] ([Id_game])
GO
ALTER TABLE [dbo].[Copy] CHECK CONSTRAINT [FK_Copy_Game]
GO
ALTER TABLE [dbo].[Copy]  WITH CHECK ADD  CONSTRAINT [FK_Copy_Player] FOREIGN KEY([Player_owner_id])
REFERENCES [dbo].[Player] ([Id_player])
GO
ALTER TABLE [dbo].[Copy] CHECK CONSTRAINT [FK_Copy_Player]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Copy] FOREIGN KEY([Copy_id])
REFERENCES [dbo].[Copy] ([Id_copy])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Copy]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Player] FOREIGN KEY([Player_borrower_id])
REFERENCES [dbo].[Player] ([Id_player])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Player]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK_Player_User] FOREIGN KEY([User_id])
REFERENCES [dbo].[User] ([Id_user])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Player_User]
GO
USE [master]
GO
ALTER DATABASE [GamesDB] SET  READ_WRITE 
GO
