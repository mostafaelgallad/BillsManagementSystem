USE [master]
GO
/****** Object:  Database [DTSAssignment]    Script Date: 4/9/2021 4:08:56 AM ******/
CREATE DATABASE [DTSAssignment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DTSAssignment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DTSAssignment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DTSAssignment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DTSAssignment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DTSAssignment] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DTSAssignment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DTSAssignment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DTSAssignment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DTSAssignment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DTSAssignment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DTSAssignment] SET ARITHABORT OFF 
GO
ALTER DATABASE [DTSAssignment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DTSAssignment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DTSAssignment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DTSAssignment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DTSAssignment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DTSAssignment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DTSAssignment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DTSAssignment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DTSAssignment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DTSAssignment] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DTSAssignment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DTSAssignment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DTSAssignment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DTSAssignment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DTSAssignment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DTSAssignment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DTSAssignment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DTSAssignment] SET RECOVERY FULL 
GO
ALTER DATABASE [DTSAssignment] SET  MULTI_USER 
GO
ALTER DATABASE [DTSAssignment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DTSAssignment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DTSAssignment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DTSAssignment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DTSAssignment] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DTSAssignment', N'ON'
GO
ALTER DATABASE [DTSAssignment] SET QUERY_STORE = OFF
GO
USE [DTSAssignment]
GO
/****** Object:  Table [dbo].[BILDTL]    Script Date: 4/9/2021 4:08:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILDTL](
	[DTLCOD] [int] IDENTITY(1,1) NOT NULL,
	[BILCOD] [int] NOT NULL,
	[ITMCOD] [int] NOT NULL,
	[ITMPRC] [decimal](10, 2) NOT NULL,
	[ITMQTY] [int] NOT NULL,
 CONSTRAINT [PK_BILDTL] PRIMARY KEY CLUSTERED 
(
	[DTLCOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILHDR]    Script Date: 4/9/2021 4:08:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILHDR](
	[BILCOD] [int] IDENTITY(1,1) NOT NULL,
	[BILDAT] [date] NOT NULL,
	[VNDCOD] [int] NOT NULL,
	[BILPRC] [decimal](10, 2) NULL,
	[BILIMG] [nvarchar](250) NULL,
 CONSTRAINT [PK_BILHDR] PRIMARY KEY CLUSTERED 
(
	[BILCOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITMDTL]    Script Date: 4/9/2021 4:08:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITMDTL](
	[ITMCOD] [int] IDENTITY(1,1) NOT NULL,
	[ITMNAM] [nvarchar](100) NOT NULL,
	[ITMPRC] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_ITMDTL] PRIMARY KEY CLUSTERED 
(
	[ITMCOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VNDDTL]    Script Date: 4/9/2021 4:08:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VNDDTL](
	[VNDCOD] [int] IDENTITY(1,1) NOT NULL,
	[VNDNAM] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_VNDDTL] PRIMARY KEY CLUSTERED 
(
	[VNDCOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BILDTL]  WITH CHECK ADD  CONSTRAINT [FK_BILDTL_BILHDR] FOREIGN KEY([BILCOD])
REFERENCES [dbo].[BILHDR] ([BILCOD])
GO
ALTER TABLE [dbo].[BILDTL] CHECK CONSTRAINT [FK_BILDTL_BILHDR]
GO
ALTER TABLE [dbo].[BILDTL]  WITH CHECK ADD  CONSTRAINT [FK_BILDTL_ITMDTL] FOREIGN KEY([ITMCOD])
REFERENCES [dbo].[ITMDTL] ([ITMCOD])
GO
ALTER TABLE [dbo].[BILDTL] CHECK CONSTRAINT [FK_BILDTL_ITMDTL]
GO
ALTER TABLE [dbo].[BILHDR]  WITH CHECK ADD  CONSTRAINT [FK_BILHDR_VNDDTL] FOREIGN KEY([VNDCOD])
REFERENCES [dbo].[VNDDTL] ([VNDCOD])
GO
ALTER TABLE [dbo].[BILHDR] CHECK CONSTRAINT [FK_BILHDR_VNDDTL]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Detail Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILDTL', @level2type=N'COLUMN',@level2name=N'DTLCOD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILDTL', @level2type=N'COLUMN',@level2name=N'BILCOD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Item Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILDTL', @level2type=N'COLUMN',@level2name=N'ITMCOD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Item Price' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILDTL', @level2type=N'COLUMN',@level2name=N'ITMPRC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Item Quantity' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILDTL', @level2type=N'COLUMN',@level2name=N'ITMQTY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILHDR', @level2type=N'COLUMN',@level2name=N'BILCOD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILHDR', @level2type=N'COLUMN',@level2name=N'BILDAT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vendor Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILHDR', @level2type=N'COLUMN',@level2name=N'VNDCOD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Price' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILHDR', @level2type=N'COLUMN',@level2name=N'BILPRC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Image Path' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BILHDR', @level2type=N'COLUMN',@level2name=N'BILIMG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Item Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ITMDTL', @level2type=N'COLUMN',@level2name=N'ITMCOD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Item Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ITMDTL', @level2type=N'COLUMN',@level2name=N'ITMNAM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Item Price' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ITMDTL', @level2type=N'COLUMN',@level2name=N'ITMPRC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Vendor Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VNDDTL', @level2type=N'COLUMN',@level2name=N'VNDCOD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vendor Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VNDDTL', @level2type=N'COLUMN',@level2name=N'VNDNAM'
GO
USE [master]
GO
ALTER DATABASE [DTSAssignment] SET  READ_WRITE 
GO
