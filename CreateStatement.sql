USE [master]
GO
/****** Object:  Database [ApartmentDB]    Script Date: 26.07.2015 12:57:05 ******/
CREATE DATABASE [ApartmentDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ApartmentDB', FILENAME = N'E:\Programme\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ApartmentDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ApartmentDB_log', FILENAME = N'E:\Programme\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ApartmentDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ApartmentDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ApartmentDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ApartmentDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ApartmentDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ApartmentDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ApartmentDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ApartmentDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ApartmentDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ApartmentDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ApartmentDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ApartmentDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ApartmentDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ApartmentDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ApartmentDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ApartmentDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ApartmentDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ApartmentDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ApartmentDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ApartmentDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ApartmentDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ApartmentDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ApartmentDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ApartmentDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ApartmentDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ApartmentDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ApartmentDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ApartmentDB] SET  MULTI_USER 
GO
ALTER DATABASE [ApartmentDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ApartmentDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ApartmentDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ApartmentDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ApartmentDB', N'ON'
GO
USE [ApartmentDB]
GO
/****** Object:  Table [dbo].[Apartment]    Script Date: 26.07.2015 12:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Apartment](
	[ApartmentID] [int] IDENTITY(1,1) NOT NULL,
	[PLZ] [char](5) NOT NULL,
	[Place] [nvarchar](70) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[Streetnumber] [nvarchar](7) NOT NULL,
	[Price] [decimal](18, 2) NULL,
	[Size] [tinyint] NULL,
	[Startdate] [date] NULL,
	[Enddate] [date] NULL,
	[Comment] [nvarchar](max) NULL,
	[StateID] [int] NOT NULL,
	[ApartmentKindID] [int] NOT NULL,
	[RenterID] [int] NOT NULL,
	[RatingID] [int] NULL,
	[Source] [nvarchar](100) NULL,
 CONSTRAINT [PK_Apartment] PRIMARY KEY CLUSTERED 
(
	[ApartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApartmentKind]    Script Date: 26.07.2015 12:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApartmentKind](
	[ApartmentKindID] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AppartmentKind] PRIMARY KEY CLUSTERED 
(
	[ApartmentKindID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rating]    Script Date: 26.07.2015 12:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingID] [int] NOT NULL,
	[Description] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[RatingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Renter]    Script Date: 26.07.2015 12:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Renter](
	[RenterID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[EMail] [nvarchar](70) NULL,
	[Mobilenumber] [nvarchar](20) NULL,
	[Telephonenumber] [nvarchar](20) NULL,
 CONSTRAINT [PK_Renter] PRIMARY KEY CLUSTERED 
(
	[RenterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[State]    Script Date: 26.07.2015 12:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateID] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartment_ApartmentKind] FOREIGN KEY([ApartmentKindID])
REFERENCES [dbo].[ApartmentKind] ([ApartmentKindID])
GO
ALTER TABLE [dbo].[Apartment] CHECK CONSTRAINT [FK_Apartment_ApartmentKind]
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartment_Rating] FOREIGN KEY([RatingID])
REFERENCES [dbo].[Rating] ([RatingID])
GO
ALTER TABLE [dbo].[Apartment] CHECK CONSTRAINT [FK_Apartment_Rating]
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartment_Renter] FOREIGN KEY([RenterID])
REFERENCES [dbo].[Renter] ([RenterID])
GO
ALTER TABLE [dbo].[Apartment] CHECK CONSTRAINT [FK_Apartment_Renter]
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartment_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[State] ([StateID])
GO
ALTER TABLE [dbo].[Apartment] CHECK CONSTRAINT [FK_Apartment_State]
GO
USE [master]
GO
ALTER DATABASE [ApartmentDB] SET  READ_WRITE 
GO
