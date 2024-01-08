USE [Izleti]
GO
/****** Object:  Database [Izleti]    Script Date: 1/8/2024 3:56:44 PM ******/
CREATE DATABASE [Izleti]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Izleti', FILENAME = N'C:\Users\aaa\Izleti - Copy.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Izleti_log', FILENAME = N'C:\Users\aaa\Izleti_log_.ldf' , SIZE = 1024KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Izleti] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Izleti].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Izleti] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Izleti] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Izleti] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Izleti] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Izleti] SET ARITHABORT OFF 
GO
ALTER DATABASE [Izleti] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Izleti] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Izleti] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Izleti] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Izleti] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Izleti] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Izleti] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Izleti] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Izleti] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Izleti] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Izleti] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Izleti] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Izleti] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Izleti] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Izleti] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Izleti] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Izleti] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Izleti] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Izleti] SET  MULTI_USER 
GO
ALTER DATABASE [Izleti] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Izleti] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Izleti] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Izleti] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Izleti] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Izleti] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Izleti', N'ON'
GO
ALTER DATABASE [Izleti] SET QUERY_STORE = ON
GO
ALTER DATABASE [Izleti] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Izleti]
GO
/****** Object:  Table [dbo].[Administrator]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrator](
	[administratorId] [int] IDENTITY(1,1) NOT NULL,
	[uporabnikId] [int] NULL,
	[Ime] [varchar](45) NULL,
	[Priimek] [varchar](45) NULL,
	[SteviloOdstranjenih] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[administratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlavnoOkno]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlavnoOkno](
	[glavnoOknoId] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[glavnoOknoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hoteli]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hoteli](
	[hoteliId] [int] IDENTITY(1,1) NOT NULL,
	[uporabnikId] [int] NULL,
	[rezervacijaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[hoteliId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Koledar]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Koledar](
	[koledarId] [int] IDENTITY(1,1) NOT NULL,
	[Datum] [date] NULL,
	[DatumKonec] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[koledarId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListHotelov]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListHotelov](
	[hotelId] [int] IDENTITY(1,1) NOT NULL,
	[ime] [nvarchar](100) NULL,
	[kraj] [nvarchar](100) NULL,
	[cena] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[hotelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Odgovori]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Odgovori](
	[odgovorId] [int] IDENTITY(1,1) NOT NULL,
	[potovanjeId] [int] NULL,
	[Odgovor1] [varchar](255) NULL,
	[Odgovor2] [varchar](255) NULL,
	[Odgovor3] [varchar](255) NULL,
	[Odgovor4] [varchar](255) NULL,
	[Odgovor5] [varchar](255) NULL,
	[Odgovor6] [varchar](255) NULL,
	[Odgovor7] [varchar](255) NULL,
	[Odgovor8] [varchar](255) NULL,
	[Odgovor9] [varchar](255) NULL,
	[Odgovor10] [varchar](255) NULL,
	[Odgovor11] [varchar](255) NULL,
	[Odgovor12] [varchar](255) NULL,
	[Odgovor13] [varchar](255) NULL,
	[Odgovor14] [varchar](255) NULL,
	[Odgovor15] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[odgovorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PodOkno]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PodOkno](
	[podOknoId] [int] IDENTITY(1,1) NOT NULL,
	[glavnoOknoId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[podOknoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ponudnik]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ponudnik](
	[ponudnikId] [int] IDENTITY(1,1) NOT NULL,
	[verodostojnost] [bit] NULL,
	[administratorId] [int] NULL,
	[Ime] [varchar](40) NULL,
	[Priimek] [varchar](40) NULL,
	[Starost] [int] NULL,
	[LetoRojstva] [int] NULL,
	[Naslov] [varchar](100) NULL,
	[Kraj] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ponudnikId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Potovanje]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Potovanje](
	[potovanjeId] [int] IDENTITY(1,1) NOT NULL,
	[kraj] [nvarchar](100) NULL,
	[cena] [int] NULL,
	[trajanje] [int] NULL,
	[datum] [date] NULL,
	[opis] [nvarchar](max) NULL,
	[ocene] [float] NULL,
	[Rezervirano] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[potovanjeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rezervacija]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rezervacija](
	[rezervacijaId] [int] IDENTITY(1,1) NOT NULL,
	[datumOd] [date] NULL,
	[datumDo] [date] NULL,
	[uporabnikId] [int] NULL,
	[potovanjeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[rezervacijaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SistemPlacilo]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SistemPlacilo](
	[sistemPlaciloId] [int] IDENTITY(1,1) NOT NULL,
	[znesek] [float] NULL,
	[uporabnikId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[sistemPlaciloId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Uporabnik]    Script Date: 1/8/2024 3:56:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Uporabnik](
	[uporabnikId] [int] IDENTITY(1,1) NOT NULL,
	[ime] [nvarchar](100) NULL,
	[priimek] [nvarchar](100) NULL,
	[datumRojstva] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[uporabnikId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Potovanje] ADD  CONSTRAINT [a]  DEFAULT ((0)) FOR [Rezervirano]
GO
ALTER TABLE [dbo].[Administrator]  WITH CHECK ADD FOREIGN KEY([uporabnikId])
REFERENCES [dbo].[Uporabnik] ([uporabnikId])
GO
ALTER TABLE [dbo].[Hoteli]  WITH CHECK ADD FOREIGN KEY([rezervacijaId])
REFERENCES [dbo].[Rezervacija] ([rezervacijaId])
GO
ALTER TABLE [dbo].[Hoteli]  WITH CHECK ADD FOREIGN KEY([uporabnikId])
REFERENCES [dbo].[Uporabnik] ([uporabnikId])
GO
ALTER TABLE [dbo].[Odgovori]  WITH CHECK ADD FOREIGN KEY([potovanjeId])
REFERENCES [dbo].[Potovanje] ([potovanjeId])
GO
ALTER TABLE [dbo].[PodOkno]  WITH CHECK ADD FOREIGN KEY([glavnoOknoId])
REFERENCES [dbo].[GlavnoOkno] ([glavnoOknoId])
GO
ALTER TABLE [dbo].[Ponudnik]  WITH CHECK ADD FOREIGN KEY([administratorId])
REFERENCES [dbo].[Administrator] ([administratorId])
GO
ALTER TABLE [dbo].[Rezervacija]  WITH CHECK ADD FOREIGN KEY([potovanjeId])
REFERENCES [dbo].[Potovanje] ([potovanjeId])
GO
ALTER TABLE [dbo].[Rezervacija]  WITH CHECK ADD FOREIGN KEY([uporabnikId])
REFERENCES [dbo].[Uporabnik] ([uporabnikId])
GO
ALTER TABLE [dbo].[SistemPlacilo]  WITH CHECK ADD FOREIGN KEY([uporabnikId])
REFERENCES [dbo].[Uporabnik] ([uporabnikId])
GO
USE [master]
GO
ALTER DATABASE [Izleti] SET  READ_WRITE 
GO
